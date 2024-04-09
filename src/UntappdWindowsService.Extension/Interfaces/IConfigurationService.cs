namespace UntappdWindowsService.Extension.Interfaces
{
    public interface IConfigurationService
    {
        string LogFilePath { get; }

        string UntappdWCFServiceUrlBase { get; }

        string UntappdWCFServiceUrlEndpoint { get; }

        string UntappdWCFServiceUrlFull { get; }
    }
}