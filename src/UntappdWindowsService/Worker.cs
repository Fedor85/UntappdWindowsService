using Microsoft.Extensions.Hosting;
using UntappdWindowsService.Extension;
using UntappdWindowsService.Interfaces;

namespace UntappdWindowsService
{
    public class Worker(IWindowsWCFService service, ILogger logger) : BackgroundService
    {
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                logger.IncrementCurrentLevel();
                logger.Log($"Start {Constants.ServiceName}");

                service.Initialize();
                service.RunAsync();
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
                service.StopAsync();

                logger.Log($"Stop {Constants.ServiceName}.");
                logger.DecrementCurrentLevel();
            }
            catch (Exception e)
            {
                Program.StopService(logger, e);
            }
            return base.StopAsync(cancellationToken);
        }
    }
}