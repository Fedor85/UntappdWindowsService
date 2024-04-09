using System;
using System.Linq;
using CoreWCF;
using CoreWCF.Configuration;
using CoreWCF.Description;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using UntappdWindowsService.Extension.Interfaces;
using UntappdWindowsService.Interfaces;
using UntappdWindowsService.WCFService.Services;

namespace UntappdWindowsService.WCFService
{
    public class UntappdWindowsWCFService(IConfigurationService configurationService, 
                                          ILogger logger,
                                          IClearTempFilesService clearTempFilesService) : IWindowsWCFService
    {
        private WebApplication webApplication;

        public void Initialize()
        {
            WebApplicationBuilder webApplicationBuilder = WebApplication.CreateBuilder();
            webApplicationBuilder.WebHost.UseUrls(configurationService.UntappdWCFServiceUrlBase);
            webApplicationBuilder.Services.AddServiceModelServices().AddServiceModelMetadata();

            webApplicationBuilder.Services.AddSingleton(clearTempFilesService);
            webApplicationBuilder.Services.AddSingleton<ClearTemp>();

            webApplication = webApplicationBuilder.Build();
            webApplication.UseServiceModel(AddServicesModels);
            ServiceMetadataBehavior serviceMetadataBehavior = ((IApplicationBuilder)webApplication).ApplicationServices.GetRequiredService<ServiceMetadataBehavior>();
            serviceMetadataBehavior.HttpGetEnabled = true;
        }

        public void RunAsync()
        {
            if (webApplication == null)
                throw new ApplicationException($"Call initialize {GetType().Name}");

            webApplication.RunAsync();
            logger?.Log($"Run {GetType().Name} by URL: {GetFullUsedUrls()}");
        }

        public void StopAsync()
        {
            if (webApplication == null)
                throw new ApplicationException($"{GetType().Name} is not Run");

            webApplication.StopAsync();
            logger?.Log($"Stop {GetType().Name}");
        }

        private void AddServicesModels(IServiceBuilder builder)
        {
            builder.AddService<ClearTemp>((_) => { })
                .AddServiceEndpoint<ClearTemp, IClearTempContract>(new BasicHttpBinding(), configurationService.UntappdWCFServiceUrlEndpoint);
        }

        private string GetFullUsedUrls()
        {
            return String.Join("; ", webApplication.Urls.Select(item => String.Concat(item, configurationService.UntappdWCFServiceUrlEndpoint)));
        }
    }
}