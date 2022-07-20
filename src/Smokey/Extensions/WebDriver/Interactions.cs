using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace Smokey.Extensions.WebDriver
{
    internal static class Interactions
    {
        public static IWebElement WaitWebElement(IWebDriver webDriver, string cssSelector)
        {
            return webDriver.WaitWebElement(cssSelector);
        }

        public static Action MoveToElement(IWebDriver webDriver, IWebElement webElement, bool moveToElement = true)
        {
            return moveToElement
                ? () => new Actions(webDriver).MoveToElement(webElement).Build().Perform()
                : () => { };
        }

        public static Action Click(IWebDriver webDriver, IWebElement webElement)
        {
            return () =>
            {
                bool TryClick()
                {
                    try
                    {
                        webElement.Click();
                    }
                    catch
                    {
                        return false;
                    }
                    
                    return true;
                }

                webDriver.Wait(condition: _ => TryClick());
            };
        }

        public static Action DoubleClick(IWebDriver webDriver, IWebElement webElement)
        {
            return () => new Actions(webDriver).DoubleClick(webElement).Build().Perform();
        }

        public static Action ContextClick(IWebDriver webDriver, IWebElement webElement)
        {
            return () => new Actions(webDriver).ContextClick(webElement).Build().Perform();
        }

        public static Action ClearLikeHuman(IWebElement webElement)
        {
            return () =>
            {
                for (var i = 0; i < 3; i++)
                {
                    webElement.SendKeys(Keys.Control + 'A' + Keys.Backspace);
                }
            };
        }
        
        public static Action DragAndDrop(IWebDriver webDriver, IWebElement from, IWebElement to)
        {
            return () => new Actions(webDriver)
                .ClickAndHold(from)
                .MoveToElement(to)
                .Release(to)
                .Build()
                .Perform();
        }
    }
}
