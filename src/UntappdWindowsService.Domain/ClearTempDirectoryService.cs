using System.Diagnostics;
using UntappdWindowsService.Interfaces;

namespace UntappdWindowsService.Domain
{
    public class ClearTempDirectoryService(ILogger logger) : IClearTempDirectoryService
    {
        private List<ProcessContainer> ProcessContainers = new();

        public void RegisterTempDirectoryByProcessId(int processId, string tempDirectory)
        {
            Process process = RegisteredProcess(processId, tempDirectory);
            if (process != null)
                NotifyOnProcessExits(process, () => StopProcessHandler(processId));
        }

        private void StopProcessHandler(int processId)
        {
            ProcessContainer processContainer = ProcessContainers.Find(item => item.Id == processId);
            logger.Log($"Stop process Id: {processContainer.Id} [{processContainer.Name}].", 1);
            foreach (string processTempDirectory in processContainer.TempDirectories)
                DeleteDirectory(processTempDirectory);

            ProcessContainers.Remove(processContainer);
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

            ProcessContainer processContainer = ProcessContainers.Find(item => item.Id == processId);
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
            ProcessContainers.Add(processContainer);

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