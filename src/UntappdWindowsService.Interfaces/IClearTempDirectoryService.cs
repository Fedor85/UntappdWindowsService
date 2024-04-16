namespace UntappdWindowsService.Interfaces
{
    public interface IClearTempDirectoryService
    {
        void Initialize();

        void Close();

        void RegisterTempDirectoryByProcessId(int processId, string tempDirectory);
    }
}