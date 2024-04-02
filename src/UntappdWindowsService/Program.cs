
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UntappdWindowsService.Infrastructure;

namespace UntappdWindowsService
{
    public class Program
    {
        static void Main(string[] args)
        {
            ConfigurationServices();
            HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
            builder.Services.AddWindowsService(options =>
            {
                options.ServiceName = "UntappdWindowsService";
            });

            builder.Services.AddHostedService(GetWorker);
            IHost host = builder.Build();
            host.Run();
        }

        private static void ConfigurationServices()
        {
            Global.AddService(new Logger());
        }

        private static Worker GetWorker(IServiceProvider serviceProvider)
        {
            return new Worker(new UntappdWindowsService());
        }
    }
}