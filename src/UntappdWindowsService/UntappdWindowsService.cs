using UntappdWindowsService.Infrastructure;
using UntappdWindowsService.Interfaces;

namespace UntappdWindowsService
{
    public class UntappdWindowsService: IWindowsService
    {
        private ILogger? logger = Global.GetService<ILogger>();

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