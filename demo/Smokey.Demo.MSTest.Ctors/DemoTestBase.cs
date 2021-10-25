using Microsoft.VisualStudio.TestTools.UnitTesting;
using Smokey.Extensions;

namespace Smokey.Demo.MSTest.Ctors
{
    [TestClass]
    public abstract class DemoTestBase : SmokeyTest
    {
        protected const int LoadingTime = 2500;

        private static TestRun _testRun;
        
        [AssemblyInitialize]
        public static void TestRunSetup(TestContext context)
        {
            _testRun = new TestRun
            {
                Domain = "https://www.google.ru/",
                BrowserConfiguration = new BrowserConfiguration
                {
                    BrowserType = BrowserType.Edge,
                    DriverSource = @"../net5.0",
                    Arguments = new[]
                    {
                        "--window-size=1366,768"
                    }
                }
            };
            Browser = Browser.CreateBrowser(_testRun.BrowserConfiguration);
        }

        [AssemblyCleanup]
        public static void TestRunCleanup()
        {
            Dispose();
        }

        [TestInitialize]
        public void TestStartup()
        {
            Browser.RemoveTokens();
            Browser.WebDriver.Navigate().GoToUrl(_testRun.Domain);
        }
    }
}