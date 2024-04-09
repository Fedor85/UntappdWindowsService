using NUnit.Framework;

namespace UntappdWindowsService.Test
{
    [SetUpFixture]
    public class SetUpTest
    {
        [OneTimeSetUp]
        public void SetUp()
        {
            Global.Initialize();
        }
    }
}