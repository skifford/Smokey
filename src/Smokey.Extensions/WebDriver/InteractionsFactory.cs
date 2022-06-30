using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using Smokey.Extensions.Extensions;

namespace Smokey.Extensions.WebDriver
{
    internal static class InteractionsFactory
    {
        public static IWebElement WaitWebElement(IWebDriver webDriver, string cssSelector)
        {
            return webDriver.WaitWebElement(cssSelector.Validate());
        }

        public static Action MoveToElement(IWebDriver webDriver, IWebElement webElement, bool moveToElement = true)
        {
            return moveToElement
                ? () => new Actions(webDriver).MoveToElement(webElement).Build().Perform()
                : () => { };
        }

        public static Action DoubleClick(IWebDriver webDriver, IWebElement webElement)
        {
            return () => new Actions(webDriver).DoubleClick(webElement).Build().Perform();
        }

        public static Action ContextClick(IWebDriver webDriver, IWebElement webElement)
        {
            return () => new Actions(webDriver).ContextClick(webElement).Build().Perform();
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
