namespace UntappdWindowsService.Interfaces
{
    public interface IWorkerService
    {
        void Initialize();

        void RunAsync();

        void StopAsync();
    }
}