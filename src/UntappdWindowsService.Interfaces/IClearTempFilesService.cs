namespace UntappdWindowsService.Interfaces
{
    public interface IClearTempFilesService
    {
        void RegisterProcessesIdByTempFiles(int processeId, string tempFilesPath);
    }
}