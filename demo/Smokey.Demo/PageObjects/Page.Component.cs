using OpenQA.Selenium;
using Smokey.Extensions.WebDriver;
using Smokey.Features.Caching;
using Smokey.Models;

namespace Smokey.Demo.PageObjects
{
    public partial class Page : SmokeyPageObject<Page>
    {
        public Page(IWebDriver webDriver) : base(webDriver)
        {
        }

        public Settings OpenSettings()
        {
            WebDriver.Click(Locators.Page.Settings.Self);
            
            return Pool.Get<Settings>();
        }
    }
}
