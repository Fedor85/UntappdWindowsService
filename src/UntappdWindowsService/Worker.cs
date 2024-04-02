using Microsoft.Extensions.Hosting;
using UntappdWindowsService.Infrastructure;
using UntappdWindowsService.Interfaces;

namespace UntappdWindowsService
{
    public class Worker(IWindowsService service) : BackgroundService
    {
        private IWindowsService service = service;

        private ILogger? logger = Global.GetService<ILogger>();

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                service.Start();
            }
            catch (Exception e)
            {
                StopService(e);
            }
            return base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            try
            {
                service.Stop();
            }
            catch (Exception e)
            {
                StopService(e);
            }
            return base.StopAsync(cancellationToken);
        }

        private void StopService(Exception e)
        {
            logger?.Log(e.Message);
            Environment.Exit(1);
        }
    }
}