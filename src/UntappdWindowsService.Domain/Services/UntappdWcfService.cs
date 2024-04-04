using UntappdWindowsService.Domain.Models;
using UntappdWindowsService.WcfService.Interface;

namespace UntappdWindowsService.Domain.Services
{
    internal class UntappdWcfService: IUntappdWcfService
    {
        Action<ProcesseTempFiles> RegisterProcessesIdByTempFilesChage;

        public void RegisterProcessesIdByTempFiles(string processeId, string tempFilesPath)
        {
            RegisterProcessesIdByTempFilesChage?.Invoke(GetProcesseTempFiles(processeId, tempFilesPath));
        }

        private ProcesseTempFiles GetProcesseTempFiles(string processeId, string tempFilesPath)
        {
            ProcesseTempFiles processeTempFiles = new();
            long id;
            if (Int64.TryParse(processeId, out id))
            {
                processeTempFiles.ProcesseId = id;
                processeTempFiles.TempFilesPath = tempFilesPath;
            }
            else
            {
                processeTempFiles.ErrorMesssage = $"{processeId} is not valid";
            }
            return processeTempFiles;
        }
    }
}