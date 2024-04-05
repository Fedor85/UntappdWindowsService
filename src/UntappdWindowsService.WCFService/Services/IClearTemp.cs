using CoreWCF;

namespace UntappdWindowsService.WCFService.Services
{
    [ServiceContract]
    public interface IClearTemp
    {
        [OperationContract]
        void RegisterProcessesIdByTempFiles(string processeId, string tempFilesPath);
    }
}