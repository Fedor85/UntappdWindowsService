using System.ServiceModel;

namespace UntappdWindowsService.WcfService.Interface
{
    [ServiceContract]
    public interface IUntappdWcfService
    {

        [OperationContract]
        void RegisterProcessesIdByTempFiles(string processeId, string tempFilesPath);
    }
}