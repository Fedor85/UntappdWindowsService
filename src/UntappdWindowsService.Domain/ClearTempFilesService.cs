using System.Diagnostics;
using UntappdWindowsService.Interfaces;

namespace UntappdWindowsService.Domain
{
    public class ClearTempFilesService(ILogger logger) : IClearTempFilesService
    {
        public void RegisterProcessesIdByTempFiles(int processeId, string tempFilesPath)
        {
            Process process = GetProcess(processeId);
            if (process == null)
            {
                logger.Log($"ProcesseId: {processeId} not found.", 1);
                return;
            }

            logger.Log($"Registered processe Id: {processeId} [{process.ProcessName}].", 1);
            logger.Log($"Temp directory: {tempFilesPath}.", 2);
            NotifyOnProcessExits(process, () => StopProcessHandler(processeId, process.ProcessName, tempFilesPath));
        }

        private void StopProcessHandler(int processeId, string processName, string directoryPath)
        {
            logger.Log($"Stop processe Id: {processeId} [{processName}].", 1);
            DeleteDirectory(directoryPath);
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

        private Process GetProcess(int processeId)
        {
            try
            {
                return Process.GetProcessById(processeId);
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
