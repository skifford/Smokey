using DotNetEnv;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Smokey.Demo.MSTest.Ctors.Extensions;
using Smokey.Extensions;

namespace Smokey.Demo.MSTest.Ctors
{
    [TestClass]
    public abstract class DemoTestBase : SmokeyTest
    {
        private const string RemoteHostName = "REMOTE_HOST";
        
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
                    BrowserType = BrowserType.Chrome,
                    DriverSource = @"../net5.0",
                    Arguments = new[]
                    {
                        "--window-size=1366,768"
                    },
                    RemoteHost = "http://localhost:4444/" // Can be open in browser
                }
            };
            
            var remoteHost = Env.GetString(RemoteHostName);
            
            var browserConfiguration = remoteHost.HasValue() 
                ? _testRun.BrowserConfiguration with { RemoteHost = remoteHost }
                : _testRun.BrowserConfiguration;
            
            Browser = Browser.CreateBrowser(browserConfiguration);
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