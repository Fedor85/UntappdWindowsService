using UntappdWindowsService.Interfaces;

namespace UntappdWindowsService.Infrastructure
{
    public class ConfigurationService: IConfigurationService
    {
        public string LopFilePath { get; } = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{Constants.ServicName}Log.txt");
    }
}