using System;
using System.IO;
using System.Threading.Tasks;
using DotNetEnv;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Smokey.Demo.MSTest.DI.Extensions;
using Smokey.DependencyInjection;
using Smokey.Extensions.Browser;
using Smokey.Models;

namespace Smokey.Demo.MSTest.DI
{
    [TestClass]
    public abstract class DemoTestBase : SmokeyTest
    {
        private const string AppSettings = "testrun.settings.json";
        private const string RemoteHostName = "REMOTE_HOST";
        
        protected const int LoadingTime = 2500;
        
        protected static TestRun TestRun;

        [AssemblyInitialize]
        public static async Task TestRunSetup(TestContext context)
        {
            var services = new ServiceCollection();
            
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory)?.FullName)
                .AddJsonFile(AppSettings, optional:false, reloadOnChange:true)
                .Build();

            ConfigureServices(services, configuration);

            await using var serviceProvider = services.BuildServiceProvider();

            ConfigureTestRun(serviceProvider);
            ConfigureBrowser();
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
            Browser.WebDriver.Navigate().GoToUrl(TestRun.Domain);
        }
        
        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSmokey(configuration);
        }

        private static void ConfigureTestRun(IServiceProvider serviceProvider)
        {
            TestRun = serviceProvider.GetService<IOptions<TestRun>>()?.Value;
            
            if (TestRun is null)
            {
                throw new ApplicationException();
            }
        }

        private static void ConfigureBrowser()
        {
            var remoteHost = Env.GetString(RemoteHostName);
            
            var browserConfiguration = remoteHost.HasValue() 
                ? TestRun.BrowserConfiguration with { RemoteHost = remoteHost }
                : TestRun.BrowserConfiguration;
            
            Browser = Browser.CreateBrowser(browserConfiguration);
        }
    }
}