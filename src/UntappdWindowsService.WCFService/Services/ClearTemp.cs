using UntappdWindowsService.Extension.Interfaces;
using UntappdWindowsService.Interfaces;

namespace UntappdWindowsService.WCFService.Services
{
    public class ClearTemp(IClearTempFilesService clearTempFilesService): IClearTempContract
    {
        public void RegisterProcessesIdByTempFiles(int processeId, string tempFilesPath)
        {
            clearTempFilesService.RegisterProcessesIdByTempFiles(processeId, tempFilesPath);
        }
    }
}