using System;
using System.Linq;
using CoreWCF;
using CoreWCF.Configuration;
using CoreWCF.Description;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using UntappdWindowsService.Interfaces;
using UntappdWindowsService.WCFService.Services;

namespace UntappdWindowsService.WCFService
{
    public class UntappdWindowsWCFService(IConfigurationService configurationService, ILogger logger) : IWindowsWCFService
    {
        private WebApplication webApplication;

        public void Initialize()
        {
            WebApplicationBuilder webApplicationBuilder = WebApplication.CreateBuilder();
            webApplicationBuilder.WebHost.UseUrls("http://localhost:5555");
            webApplicationBuilder.Services.AddServiceModelServices().AddServiceModelMetadata();

            if(logger != null)
                webApplicationBuilder.Services.AddSingleton(logger);

            webApplication = webApplicationBuilder.Build();
            webApplication.UseServiceModel(AddServicesModels);
            ServiceMetadataBehavior serviceMetadataBehavior = ((IApplicationBuilder)webApplication).ApplicationServices.GetRequiredService<ServiceMetadataBehavior>();
            serviceMetadataBehavior.HttpGetEnabled = true;
        }

        public void RunAsync()
        {
            if (webApplication == null)
                throw new ApplicationException("Call initialize UntappdWindowsWCFService");

            webApplication.RunAsync();
            logger?.Log($"Run WCFService by URL: {String.Join("; ", webApplication.Urls.Select(item =>String.Concat(item, ClearTemp.ServiceEndpoint)))}");
        }

        public void Run()
        {
            if (webApplication == null)
                throw new ApplicationException("Call initialize UntappdWindowsWCFService");

            webApplication.Run();
        }

        public void StopAsync()
        {
            webApplication.StopAsync();
            logger?.Log($"Stop WCFService");
        }

        private static void AddServicesModels(IServiceBuilder builder)
        {
            builder.AddService<ClearTemp>((_) => { })
                .AddServiceEndpoint<ClearTemp, IClearTemp>(new BasicHttpBinding(), ClearTemp.ServiceEndpoint);
        }
    }
}