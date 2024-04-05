
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UntappdWindowsService.Infrastructure;
using UntappdWindowsService.Interfaces;
using UntappdWindowsService.WCFService;

namespace UntappdWindowsService
{
    public class Program
    {
        static void Main(string[] args)
        {
            HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
            AddServices(builder);
            
            builder.Services.AddWindowsService(ConfigureWindowsService);
            builder.Services.AddHostedService<Worker>();

            IHost host = builder.Build();
            host.Run();
        }

        private static void AddServices(IHostApplicationBuilder hostApplicationBuilder)
        {
            hostApplicationBuilder.Services.AddSingleton<IConfigurationService, ConfigurationService>();
            hostApplicationBuilder.Services.AddSingleton<ILogger, Logger>();
            hostApplicationBuilder.Services.AddSingleton<IWindowsWCFService, UntappdWindowsWCFService>();
        }

        private static void ConfigureWindowsService(WindowsServiceLifetimeOptions options)
        {
            options.ServiceName = Constants.ServicName;
        }

        public static void StopService(ILogger logger, Exception e)
        {
            logger?.Log($"Stop\\Exit Service: {e.Message}");
            Environment.Exit(1);
        }
    }
}