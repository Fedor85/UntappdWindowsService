using UntappdWindowsService.Extension.Interfaces;

namespace UntappdWindowsService.Extension.Services
{
    public class ConfigurationService : IConfigurationService
    {
        public string LogFilePath { get; protected set; } = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{Constants.ServiceName}Log.txt");
        
        public string UntappdWCFServiceUrlBase { get; protected set; } = Constants.UntappdWCFServiceUrlBase;

        public string UntappdWCFServiceUrlEndpoint { get; protected set; } = Constants.UntappdWCFServiceUrlEndpoint;

        public string UntappdWCFServiceUrlFull { get; protected set; } = Constants.UntappdWCFServiceUrlFull;
    }
}