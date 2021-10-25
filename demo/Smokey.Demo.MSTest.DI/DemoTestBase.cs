using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Smokey.Demo.MSTest.DI.Logger;
using Smokey.Extensions;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace Smokey.Demo.MSTest.DI
{
    [TestClass]
    public abstract class DemoTestBase : SmokeyTest
    {
        protected const int LoadingTime = 2500;
        
        protected static TestRun _testRun;

        [AssemblyInitialize]
        public static async Task TestRunSetup(TestContext context)
        {
            const string appSettings = "testrun.effective.json";
            
            var services = new ServiceCollection();
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory)?.FullName)
                .AddJsonFile(appSettings, optional:false, reloadOnChange:true)
                .Build();

            ConfigureServices(services, configuration);

            await using var serviceProvider = services.BuildServiceProvider();
            _testRun = serviceProvider.GetService<IOptions<TestRun>>()?.Value;
            Browser = Browser.CreateBrowser(_testRun?.BrowserConfiguration);
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
        
        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSmokey(configuration);
            services.AddSingleton<ILogger, CustomLogger>();
        }
    }
}