using System.Diagnostics;
using UntappdWindowsService.Extension.Interfaces;
using UntappdWindowsService.Infrastructure;
using UntappdWindowsService.Interfaces;

namespace UntappdWindowsService.Domain
{
    public class ClearTempDirectoryService(IConfigurationService configurationService, ILogger logger) : IClearTempDirectoryService
    {
        private readonly string backupFile = FileHelper.GetFilePath(configurationService.ProcessTempDirectoryBackupFilePath);

        private List<ProcessContainer> processContainers = new();

        public void Initialize()
        {
            processContainers.Clear();
            LoadBackupProcessContainers();
        }

        public void Close()
        {
            if (processContainers.Count > 0)
            {
                foreach (ProcessContainer process in processContainers)
                {
                    logger.Log($"Save process Id: {process.Id} [{process.Name}].", 1);
                    foreach (string directory in process.TempDirectories)
                        logger.Log($"Temp directory: {directory}.", 2);
                } 
            }
            FileHelper.SaveFile(backupFile, processContainers);
        }

        private void LoadBackupProcessContainers()
        {
            List<ProcessContainer> processContainers = FileHelper.OpenFileToList<ProcessContainer>(backupFile);
            if (processContainers == null || processContainers.Count == 0)
                return;

            logger.Log($"Loaded by {backupFile}", 1);
            foreach (ProcessContainer processContainer in processContainers)
            {
                Process process = GetProcess(processContainer.Id);
                if (process == null || !process.ProcessName.Equals(processContainer.Name))
                {
                    logger.Log($"Process {processContainer.Id} [{processContainer.Name}] not found or is different.", 1);
                    foreach (string directory in processContainer.TempDirectories)
                        DeleteDirectory(directory);
                }
                else
                {
                    foreach (string directory in processContainer.TempDirectories)
                        RegisterTempDirectoryByProcessId(processContainer.Id, directory);
                }
            }
        }

        public void RegisterTempDirectoryByProcessId(int processId, string tempDirectory)
        {
            Process process = RegisteredProcess(processId, tempDirectory);
            if (process != null)
                NotifyOnProcessExits(process, () => StopProcessHandler(processId));
        }

        private void StopProcessHandler(int processId)
        {
            ProcessContainer processContainer = processContainers.Find(item => item.Id == processId);
            logger.Log($"Stop process Id: {processContainer.Id} [{processContainer.Name}].", 1);
            foreach (string processTempDirectory in processContainer.TempDirectories)
                DeleteDirectory(processTempDirectory);

            processContainers.Remove(processContainer);
        }

        private void DeleteDirectory(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                logger.Log($"Not found temp directory: {directoryPath}.", 2);
                return;
            }

            Directory.Delete(directoryPath, true);
            logger.Log($"Delete temp directory: {directoryPath}.", 2);
        }

        private void NotifyOnProcessExits(Process process, Action action)
        {
            Task.Run(process.WaitForExit).ContinueWith(task => action());
        }

        private Process RegisteredProcess(int processId, string tempDirectory)
        {
            Process process = GetProcess(processId);
            if (process == null)
            {
                logger.Log($"ProcessId: {processId} not found.", 1);
                return null;
            }

            ProcessContainer processContainer = processContainers.Find(item => item.Id == processId);
            if (processContainer != null)
            {
                if (processContainer.TempDirectories.Contains(tempDirectory))
                {
                    logger.Log($"Process Id: {processId} [{process.ProcessName}] duplicate temp directory: {tempDirectory}", 2);
                }
                else
                {
                    processContainer.TempDirectories.Add(tempDirectory);
                    logger.Log($"Process Id: {processId} [{process.ProcessName}] add temp directory: {tempDirectory}", 2);
                }
                return null;
            }

            processContainer = new ProcessContainer(processId, process.ProcessName);
            processContainer.TempDirectories.Add(tempDirectory);
            processContainers.Add(processContainer);

            logger.Log($"Registered process Id: {processId} [{process.ProcessName}].", 1);
            logger.Log($"Temp directory: {tempDirectory}.", 2);
            return process;
        }

        private Process GetProcess(int processId)
        {
            try
            {
                return Process.GetProcessById(processId);
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}