using Microsoft.VisualStudio.TestTools.UnitTesting;
using UntappdWindowsService.Infrastructure;
using UntappdWindowsService.WCFService;

namespace UntappdWindowsService.Test
{
    [TestClass]
    public class WCFServiceFixture
    {

        [TestMethod]
        public void TestWCFService()
        {
            UntappdWindowsWCFService wcfService = new(null, new Logger(new ConfigurationService()));
            wcfService.Initialize();
            wcfService.RunAsync();
            wcfService.StopAsync();
        }
    }
}
