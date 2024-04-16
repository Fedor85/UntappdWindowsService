using Microsoft.Extensions.DependencyInjection;
using UntappdWindowsService.Client;
using UntappdWindowsService.Domain;
using UntappdWindowsService.Extension.Interfaces;
using UntappdWindowsService.Infrastructure;
using UntappdWindowsService.Interfaces;
using UntappdWindowsService.WCFService;

namespace UntappdWindowsService.Test
{
    public static class Global
    {
        public static ServiceProvider ServiceProvider { get; private set; }
        
        public static void Initialize()
        {
            ServiceCollection services = new ServiceCollection();
            services.AddSingleton<IConfigurationService, TestConfigurationService>();
            services.AddSingleton<ILogger, FileLogger>();
            services.AddSingleton<IClearTempDirectoryService, ClearTempDirectoryService>();
            services.AddSingleton<UntappdWindowsWCFService>();
            services.AddSingleton<IUntappdWindowsServiceClient, UntappdWindowsServiceClient>();
            ServiceProvider = services.BuildServiceProvider();
        }
    }
}
