using UntappdWindowsService.Interfaces;

namespace UntappdWindowsService.WCFService.Services
{
    public class ClearTemp(ILogger logger): IClearTemp
    {
        public const string ServiceEndpoint = "/UntappdWindowsService/ClearTemp";
        public void RegisterProcessesIdByTempFiles(string processeId, string tempFilesPath)
        {
            logger?.Log($"processeId: {processeId}, tempFilesPath: {tempFilesPath}");
        }
    }
}