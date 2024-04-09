namespace UntappdWindowsService.Interfaces
{
    public interface IWindowsWCFService
    {
        void Initialize();

        void RunAsync();

        void StopAsync();
    }
}