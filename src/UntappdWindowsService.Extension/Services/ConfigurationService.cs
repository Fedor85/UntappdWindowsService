using System.Configuration;
using System.Reflection;
using UntappdWindowsService.Extension.Interfaces;

namespace UntappdWindowsService.Extension.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private string UntappdWCFServiceUrlBaseKey = "UntappdWCFServiceUrlBase";

        public string LogFilePath { get; protected set; } = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{Constants.ServiceName}Log.txt");
        
        public string UntappdWCFServiceUrlBase { get; protected set; }

        public string UntappdWCFServiceUrlEndpoint { get; protected set; } = Constants.UntappdWCFServiceUrlEndpoint;

        public string UntappdWCFServiceUrlFull { get; protected set; } = Constants.UntappdWCFServiceUrlFull;

        public ConfigurationService()
        {
            UntappdWCFServiceUrlBase = GetUntappdWCFServiceUrlBase(Constants.UntappdWCFServiceUrlBase);
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

            string thisAssemblyModuleName = Assembly.GetAssembly(type).ManifestModule.Name;
            if (File.Exists($"{thisAssemblyModuleName}.config"))
            {
                Configuration thisConfiguration = ConfigurationManager.OpenExeConfiguration(thisAssemblyModuleName);
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