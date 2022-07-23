using System;
using System.IO;
using DotNetEnv;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Smokey.Demo.Extensions;
using Smokey.Demo.PageObjects;
using Smokey.DependencyInjection;
using Smokey.Extensions.Browser;
using Smokey.Features.Caching;
using Smokey.Models;

namespace Smokey.Demo
{
    [TestClass]
    public abstract class DemoTestBase : SmokeyTest
    {
        private const string AppSettings = "testrun.settings.json";
        private const string RemoteHostName = "REMOTE_HOST";
        
        private static TestRun _testRun;
        
        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext context)
        {
            var services = new ServiceCollection();
            
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory)?.FullName)
                .AddJsonFile(AppSettings, optional:false, reloadOnChange:true)
                .Build();
            
            ConfigureServices(services, configuration);
            
            _testRun = services
                .BuildServiceProvider()
                .GetRequiredService<IOptions<TestRun>>()
                .Value;
            
            ConfigureBrowser();
            ConfigurePool();
        }

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            Dispose();
        }

        [TestInitialize]
        public virtual void TestInitialize()
        {
            Browser.RemoveTokens();
            Browser.WebDriver.Navigate().GoToUrl(_testRun.Domain);
        }

        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient();
            services.AddSmokey(configuration);
        }

        private static void ConfigureBrowser()
        {
            var remoteHost = Env.GetString(RemoteHostName);
            
            Console.WriteLine($"remoteHost: {remoteHost}");
            
            var browserConfiguration = remoteHost.HasValue() 
                ? _testRun.BrowserConfiguration with { RemoteHost = remoteHost }
                : _testRun.BrowserConfiguration;
            
            Browser = Browser.CreateBrowser(browserConfiguration);
        }
        
        private static void ConfigurePool()
        {
            Pool.Add(() => new Page(Browser.WebDriver));
            Pool.Add(() => new Settings(Browser.WebDriver));
        }
    }
}
