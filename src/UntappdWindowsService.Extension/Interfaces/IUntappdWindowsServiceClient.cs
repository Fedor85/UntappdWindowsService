namespace UntappdWindowsService.Extension.Interfaces
{
    public interface IUntappdWindowsServiceClient
    {
        void SetTempFilesByProcessesId(int processeId, string tempFilesPath);

        Task SetTempFilesByProcessesIdAsync(int processeId, string tempFilesPath);
    }
}