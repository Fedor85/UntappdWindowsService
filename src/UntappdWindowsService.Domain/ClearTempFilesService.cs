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
                logger.Log($"ProcesseId: {processeId} not found");
                return;
            }

            logger.Log($"Registered processe Id: {processeId} [{process.ProcessName}]; tempFilesPath: {tempFilesPath}");
            NotifyOnProcessExits(process, () => DeleteDirectory(tempFilesPath));
        }

        private void DeleteDirectory(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
                return;

            Directory.Delete(directoryPath, true);
            logger.Log($"Delete directory: {directoryPath}");
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
