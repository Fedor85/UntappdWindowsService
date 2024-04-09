using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using UntappdWindowsService.Client;
using UntappdWindowsService.Extension.Interfaces;
using UntappdWindowsService.Interfaces;

namespace UntappdWindowsService.Test
{
    [TestClass, TestFixture]
    public class WCFServiceFixture
    {
        [TestMethod, Test]
        public void TestWCFService()
        {
            IWindowsWCFService windowsWcfService = Global.ServiceProvider.GetService<IWindowsWCFService>();
            windowsWcfService.Initialize();
            windowsWcfService.RunAsync();

            IUntappdWindowsServiceClient client = Global.ServiceProvider.GetService<IUntappdWindowsServiceClient>();
            TestClient(client);

            windowsWcfService.StopAsync();
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
            client.SetTempFilesByProcessesId(-1, tempDirectory);
            client.SetTempFilesByProcessesId(-2, tempDirectory);
            Process newProcess = Process.Start("Utils/ConsoleProcess/ConsoleProcess.exe");
            client.SetTempFilesByProcessesId(newProcess.Id, tempDirectory);

            newProcess.Kill();
            newProcess.WaitForExit();
            newProcess.Dispose();
        }
    }
}