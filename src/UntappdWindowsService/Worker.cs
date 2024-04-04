using Microsoft.Extensions.Hosting;
using UntappdWindowsService.Interfaces;

namespace UntappdWindowsService
{
    public class Worker(IWindowsService service, ILogger logger) : BackgroundService
    {
        private readonly IWindowsService service = service;

        private readonly ILogger logger = logger;

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                service.Start();
            }
            catch (Exception e)
            {
                Program.StopService(logger, e);
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
                Program.StopService(logger, e);
            }
            return base.StopAsync(cancellationToken);
        }
    }
}