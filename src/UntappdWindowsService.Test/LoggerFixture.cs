using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using UntappdWindowsService.Interfaces;

namespace UntappdWindowsService.Test
{
    [TestClass, TestFixture]
    public class LoggerFixture
    {
        [TestMethod, Test]
        public void TestLog()
        {
            ILogger logger = Global.ServiceProvider.GetService<ILogger>();
            logger.Log("TestLog");
        }
    }
}
