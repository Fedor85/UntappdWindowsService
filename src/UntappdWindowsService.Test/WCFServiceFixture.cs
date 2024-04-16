using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using UntappdWindowsService.Client;
using UntappdWindowsService.Extension.Interfaces;
using UntappdWindowsService.Interfaces;
using UntappdWindowsService.WCFService;

namespace UntappdWindowsService.Test
{
    [TestClass, TestFixture]
    public class WCFServiceFixture
    {
        [TestMethod, Test]
        public void TestWCFService()
        {
            ILogger logger = Global.ServiceProvider.GetService<ILogger>();
            logger.IncrementCurrentLevel();
            logger.Log($"Start {Extension.Constants.ServiceName}");

            UntappdWindowsWCFService windowsWcfService = Global.ServiceProvider.GetService<UntappdWindowsWCFService>();
            windowsWcfService.Initialize();
            windowsWcfService.RunAsync();

            IUntappdWindowsServiceClient client = Global.ServiceProvider.GetService<IUntappdWindowsServiceClient>();
            TestClient(client);
            windowsWcfService.StopAsync();

            logger.Log($"Stop {Extension.Constants.ServiceName}.");
            logger.DecrementCurrentLevel();
        }

        [TestMethod, Test]
        public void TestPublishWCFService()
        {
            IUntappdWindowsServiceClient client = new UntappdWindowsServiceClient();
            TestClient(client);
        }

        private void TestClient(IUntappdWindowsServiceClient client)
        {
            string tempDirectory = Path.Combine(TestHelper.GetSolutionDirectory(), Constants.TestFolder, "TestTemp");
            TestHelper.CreateTempFiles(tempDirectory, 5);
            client.SetTempDirectoryByProcessId(-1, tempDirectory);
            client.SetTempDirectoryByProcessId(-2, tempDirectory);

            Process newProcess = Process.Start("Utils/ConsoleProcess/ConsoleProcess.exe");

            client.SetTempDirectoryByProcessId(newProcess.Id, tempDirectory);
            client.SetTempDirectoryByProcessId(newProcess.Id, tempDirectory);
            client.SetTempDirectoryByProcessId(newProcess.Id, Path.Combine(tempDirectory, "FakeFolder"));

            newProcess.Kill();
            newProcess.WaitForExit();
            newProcess.Dispose();
            Thread.Sleep(3000);
        }
    }
}