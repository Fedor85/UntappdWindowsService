using UntappdWindowsService.Interfaces;

namespace UntappdWindowsService
{
    public class UntappdWindowsService(ILogger logger) : IWindowsService
    {
        private readonly ILogger logger = logger;

        public void Start()
        {
            logger?.Log("Start UntappdWindowsService.");
        }

        public void Stop()
        {
            logger?.Log("Stop UntappdWindowsService.");
        }
    }
}