using UntappdWindowsService.Extension.Interfaces;
using UntappdWindowsService.Interfaces;

namespace UntappdWindowsService.WCFService.Services
{
    public class ClearTemp(IClearTempDirectoryService clearTempDirectoryService): IClearTempContract
    {
        public void RegisterTempDirectoryByProcessId(int processId, string tempFilesPath)
        {
            clearTempDirectoryService.RegisterTempDirectoryByProcessId(processId, tempFilesPath);
        }
    }
}