using Microsoft.VisualStudio.TestTools.UnitTesting;
using UntappdWindowsService.Infrastructure;
using UntappdWindowsService.Interfaces;

namespace UntappdWindowsService.Test
{
    [TestClass]
    public class LoggerFixture
    {
        [TestMethod]
        public void TestLog()
        {
            ILogger logger = new Logger();
            logger.Log("TestLog");
        }
    }
}
