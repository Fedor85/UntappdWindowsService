namespace UntappdWindowsService.Extension.Interfaces
{
    [System.ServiceModel.ServiceContract, CoreWCF.ServiceContract]
    public interface IClearTempContract
    {
        [System.ServiceModel.OperationContract, CoreWCF.OperationContract]
        void RegisterProcessesIdByTempFiles(int processeId, string tempFilesPath);
    }
}