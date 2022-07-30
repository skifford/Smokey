using System.Linq;
using OpenQA.Selenium;
using Smokey.Guarding.ExceptionMessages;

namespace Smokey.Extensions.WebDriver
{
    public static class DataExtractionWebDriverExtensions
    {
        public static string Text(this IWebDriver webDriver, string cssSelector)
        {
            return webDriver.WaitWebElement(cssSelector).Text;
        }
        
        public static string Attribute(this IWebDriver webDriver, string cssSelector, string attribute)
        {
            return webDriver
                .WaitWebElement(cssSelector)
                .GetAttribute(attribute.NotEmpty(Validation.NullOrWhiteSpace(nameof(attribute))));
        }
        
        public static string CssValue(this IWebDriver webDriver, string cssSelector, string cssValue)
        {
            return webDriver
                .WaitWebElement(cssSelector)
                .GetCssValue(cssValue.NotEmpty(Validation.NullOrWhiteSpace(nameof(cssValue))));
        }
        
        public static bool Exist(this IWebDriver webDriver, string cssSelector)
        {
            return webDriver.FindElements(By.CssSelector(cssSelector)).Any();
        }
    }
}
