namespace UntappdWindowsService.Extension
{
    public static class Constants
    {
        public const string ServiceName = "UntappdWindowsService";

        public const string UntappdWCFServiceUrlBase = "http://localhost:5555";

        public const string UntappdWCFServiceUrlEndpoint = $"/{ServiceName}/ClearTemp";

        public const string UntappdWCFServiceUrlFull = $"{UntappdWCFServiceUrlBase}{UntappdWCFServiceUrlEndpoint}";
    }
}