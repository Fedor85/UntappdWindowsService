namespace UntappdWindowsService.Extension.Interfaces
{
    public interface IUntappdWindowsServiceClient
    {
        void SetTempDirectoryByProcessId(int processId, string tempDirectory);

        Task SetTempDirectoryByProcessIdAsync(int processId, string tempDirectory);
    }
}