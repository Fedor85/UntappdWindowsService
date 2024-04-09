using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UntappdWindowsService.Domain;
using UntappdWindowsService.Extension;
using UntappdWindowsService.Extension.Interfaces;
using UntappdWindowsService.Extension.Services;
using UntappdWindowsService.Infrastructure;
using UntappdWindowsService.Interfaces;
using UntappdWindowsService.WCFService;
using ILogger = UntappdWindowsService.Interfaces.ILogger;

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
            hostApplicationBuilder.Services.AddSingleton<ILogger, FileLogger>();
            hostApplicationBuilder.Services.AddSingleton<IClearTempFilesService, ClearTempFilesService>();
            hostApplicationBuilder.Services.AddSingleton<IWindowsWCFService, UntappdWindowsWCFService>();
        }

        private static void ConfigureWindowsService(WindowsServiceLifetimeOptions options)
        {
            options.ServiceName = Constants.ServiceName;
        }

        public static void StopService(ILogger logger, Exception e)
        {
            logger?.Log($"Stop\\Exit {Constants.ServiceName}: {e.Message}");
            Environment.Exit(1);
        }
    }
}