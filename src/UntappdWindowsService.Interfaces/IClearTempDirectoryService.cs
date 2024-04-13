namespace UntappdWindowsService.Interfaces
{
    public interface IClearTempDirectoryService
    {
        void RegisterTempDirectoryByProcessId(int processId, string tempDirectory);
    }
}