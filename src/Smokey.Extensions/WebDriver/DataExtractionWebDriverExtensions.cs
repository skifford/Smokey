using System.Linq;
using OpenQA.Selenium;
using Smokey.Extensions.Extensions;

namespace Smokey.Extensions.WebDriver
{
    public static class DataExtractionWebDriverExtensions
    {
        public static string Text(this IWebDriver webDriver, string cssSelector)
        {
            return webDriver.WaitWebElement(cssSelector.Validate()).Text;
        }
        
        public static string GetAttribute(this IWebDriver webDriver, string cssSelector, string attribute)
        {
            return webDriver
                .WaitWebElement(cssSelector.Validate())
                .GetAttribute(attribute.Validate());
        }
        
        public static bool Exist(this IWebDriver webDriver, string cssSelector)
        {
            return webDriver.FindElements(By.CssSelector(cssSelector.Validate())).Any();
        }
    }
}
