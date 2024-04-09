using UntappdWindowsService.Extension.Services;

namespace UntappdWindowsService.Test
{
    public class TestConfigurationService: ConfigurationService
    {
        public TestConfigurationService()
        {
            LogFilePath = Path.Combine(TestHelper.GetSolutionDirectory(), Constants.TestFolder, "TestLog.txt");

            UntappdWCFServiceUrlBase = "http://localhost:5556";

            UntappdWCFServiceUrlEndpoint = $"/{Extension.Constants.ServiceName}Test/ClearTemp";

            UntappdWCFServiceUrlFull = $"{UntappdWCFServiceUrlBase}{UntappdWCFServiceUrlEndpoint}";
        }
    }
}