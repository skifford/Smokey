using OpenQA.Selenium;
using Smokey.Extensions.WebDriver;
using Smokey.Features.Caching;
using Smokey.Models;

namespace Smokey.Demo.PageObjects
{
    public class Settings : SmokeyPageObject<Settings>
    {
        public Settings(IWebDriver webDriver) : base(webDriver)
        {
        }

        public Page ToggleDarkTheme()
        {
            WebDriver.Click(Locators.Page.Settings.ToggleDarkTheme);
            
            return Pool.Get<Page>();
        }
    }
}
