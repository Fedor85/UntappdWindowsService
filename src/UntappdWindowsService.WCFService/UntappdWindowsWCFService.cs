using System;
using System.Linq;
using System.Reflection;
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
                                          IClearTempDirectoryService clearTempDirectoryService) : IWindowsWCFService
    {

        private string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();

        private WebApplication webApplication;

        public void Initialize()
        {
            WebApplicationBuilder webApplicationBuilder = WebApplication.CreateBuilder();
            webApplicationBuilder.WebHost.UseUrls(configurationService.UntappdWCFServiceUrlBase);
            webApplicationBuilder.Services.AddServiceModelServices().AddServiceModelMetadata();

            webApplicationBuilder.Services.AddSingleton(clearTempDirectoryService);
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
            logger?.IncrementCurrentLevel();
            logger?.Log(GetMessage("Run"));
        }

        public void StopAsync()
        {
            if (webApplication == null)
                throw new ApplicationException($"{GetType().Name} is not Run");


            logger?.Log(GetMessage("Stop"));
            logger?.DecrementCurrentLevel();
            webApplication.StopAsync();
        }

        private void AddServicesModels(IServiceBuilder builder)
        {
            builder.AddService<ClearTemp>((_) => { })
                .AddServiceEndpoint<ClearTemp, IClearTempContract>(new BasicHttpBinding(), configurationService.UntappdWCFServiceUrlEndpoint);
        }

        private string GetMessage(string processesName)
        {
            return $"{processesName} {GetType().Name} [version: {version}] by URL: {GetFullUsedUrls()}.";
        }

        private string GetFullUsedUrls()
        {
            return String.Join("; ", webApplication.Urls.Select(item => String.Concat(item, configurationService.UntappdWCFServiceUrlEndpoint)));
        }
    }
}