using Microsoft.Extensions.Hosting;
using System.Reflection;
using UntappdWindowsService.Extension;
using UntappdWindowsService.Interfaces;

namespace UntappdWindowsService
{
    public class Worker(IWorkerService service, ILogger logger) : BackgroundService
    {
        private string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                logger.IncrementCurrentLevel();
                logger.Log(GetMessage("Start"));

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

                logger.Log(GetMessage("Stop"));
                logger.DecrementCurrentLevel();
            }
            catch (Exception e)
            {
                Program.StopService(logger, e);
            }
            return base.StopAsync(cancellationToken);
        }

        private string GetMessage(string processesName)
        {
            return $"{processesName} {Constants.ServiceName} [version: {version}].";
        }
    }
}