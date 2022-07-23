# Smokey
Smokey is an alternative UI test tools that wraps Selenium.

## Features
- Agile configuring test-runs via app or json file is supported: browser type, driver version, screen resolution and etc.
- Usage in docker containers is supported.
- Creating Smokey objects via constructors or via dependency injections.
- Instance caching via pool is supported (similar to DI, but not quite).
- Several methods are supported for saving application state into singleton values storage during test execution:
directly storing a key-value pair, storing multiple values using attributes, and saving a value at read/write
time using a property decorator. 
- Also searching for data in the value store in the LINQ style is supported 
(see more: Demo/Smokey.Demo.Features.ValuesCollecting project).


## Structure of solution
- **Demo**
    Contains demo projects with examples of Smokey usage.
    At moment there are examples for MSTest framework.
- **Smokey** 
    Contains core classes.

## Quick start
For example, MSTest framework is shown.
1. Install smokey (nuget or locally).
2. Create your own base test class and inherite it of SmokeyTest class:
```csharp
    [TestClass]
    public abstract class TestBase : SmokeyTest
    {   
        protected static TestRun TestRun { get; private set; };
        
        [AssemblyInitialize]
        public static void TestRunSetup(TestContext context)
        {
            TestRun = new TestRun
            {
                Domain = "https://www.google.ru/",
                BrowserConfiguration = new BrowserConfiguration
                {
                    BrowserType = BrowserType.Chrome,
                    DriverSource = @"../net5.0",
                    Arguments = new[]
                    {
                        "--window-size=1366,768"
                    }
                }
            };
            
            Browser = Browser.CreateBrowser(TestRun.BrowserConfiguration);
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
```
3. Use properties Browser and TestRun in your tests.
4. You are awesome!

## Features
A localization test for a one page might look like this:
```csharp
        public void Localization_Ru()
        {
            // Arrange
            var page = new Page(Browser.WebDriver);
            
            var expected = new List<string>()
            {
                Localization.For(Language.Ru).About,
                Localization.For(Language.Ru).Ads,
                Localization.For(Language.Ru).Services,
                Localization.For(Language.Ru).HowSearchWorks,
                Localization.For(Language.Ru).Privacy,
                Localization.For(Language.Ru).Terms,
                Localization.For(Language.Ru).Settings
            };
            
            expected = expected.OrderBy(value => value).ToList();
            
            // Act
            page.CollectValues();

            var actual = page.Storage.By("Page.Localization").OrderBy(value => value).ToList();

            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }
```
This is possible thanks to the use of the function of collecting values through a special attribute from Smokey.
The reflection mechanism in C-Sharp allows to get access to certain members of the class, 
and the attribute indicates which ones. At the time of calling the **CollectValues()** method, all values in the properties
marked with this attribute are stored. We can set up our test so that the comparison takes one line, using the same key for
the properties we want.

See more features usage in Demo/Smokey.Demo.Features.ValuesCollecting project.

## License
MIT
