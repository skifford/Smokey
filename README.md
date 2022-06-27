# Smokey
Smokey is an alternative UI test tools that wraps Selenium.

## Features
- Supported agile configuring testruns via app or json file: browser type, driver version, screen resolution and etc.
- Supported usage in docker containers.
- Creating Smokey objects via constructors or via dependensy injections.

## Structure of solution
- **Demo**
    Contains demo projects with examples of Smokey usage.
    At moment there are examples for MSTest framework.
- **Smokey.Core** 
    Contains core classes.
- **Smokey.Extensions**
    Contains extensions for advanced and comfortable usage Smokey.

## Quick start
For example, MSTest framework is shown.
1. Install smokey (nuget or locally).
2. Create your own base test class and inherite it of SmokeyTest class:
```cs
    [TestClass]
    public abstract class TestBase : SmokeyTest
    {
        private const string RemoteHostName = "REMOTE_HOST";
    
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
            
            var browserConfiguration = !string.IsNullOrWhiteSpace(remoteHost)
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
```
3. Use property Browser in your tests.
4. You are awesome!

## License
MIT