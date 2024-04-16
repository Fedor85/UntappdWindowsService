using System.Configuration;
using System.Reflection;
using UntappdWindowsService.Extension.Interfaces;

namespace UntappdWindowsService.Extension.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private string UntappdWCFServiceUrlBaseKey = "UntappdWCFServiceUrlBase";

        public string LogFilePath { get; protected init; }

        public string ProcessTempDirectoryBackupFilePath { get; protected init; }

        public string UntappdWCFServiceUrlBase { get; protected init; }

        public string UntappdWCFServiceUrlEndpoint { get; protected init; }

        public string UntappdWCFServiceUrlFull { get; protected init; }

        public ConfigurationService()
        {
            LogFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{Constants.ServiceName}Log.txt");
            ProcessTempDirectoryBackupFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ProcessTempDirectoryBackup.pcb");
            UntappdWCFServiceUrlBase = GetUntappdWCFServiceUrlBase(Constants.UntappdWCFServiceUrlBase);
            UntappdWCFServiceUrlEndpoint = Constants.UntappdWCFServiceUrlEndpoint;
            UntappdWCFServiceUrlFull = $"{UntappdWCFServiceUrlBase}{UntappdWCFServiceUrlEndpoint}";
        }

        protected virtual string GetUntappdWCFServiceUrlBase( string defaultValue)
        {
            string entryAssemblyConfigValue = ConfigurationManager.AppSettings.Get(UntappdWCFServiceUrlBaseKey);
            if (!String.IsNullOrEmpty(entryAssemblyConfigValue))
                return entryAssemblyConfigValue;

            entryAssemblyConfigValue = GetConfigValueByBaseType(GetType());
            return !String.IsNullOrEmpty(entryAssemblyConfigValue) ? entryAssemblyConfigValue : defaultValue;
        }

        private string GetConfigValueByBaseType(Type type)
        {
            if (type == typeof(object))
                return null;

            string assemblyLocation = Assembly.GetAssembly(type).Location;
            if (File.Exists($"{assemblyLocation}.config"))
            {
                Configuration thisConfiguration = ConfigurationManager.OpenExeConfiguration(assemblyLocation);
                if (thisConfiguration.AppSettings.Settings.AllKeys.Contains(UntappdWCFServiceUrlBaseKey))
                {
                    string thisAssemblyConfigValue = thisConfiguration.AppSettings.Settings[UntappdWCFServiceUrlBaseKey].Value;
                    if (!String.IsNullOrEmpty(thisAssemblyConfigValue))
                        return thisAssemblyConfigValue;
                }
            }
            return GetConfigValueByBaseType(type.BaseType);
        }
    }
}