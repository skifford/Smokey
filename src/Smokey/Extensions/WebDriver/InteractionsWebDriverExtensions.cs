using OpenQA.Selenium;
using Smokey.Guarding.ExceptionMessages;

// ReSharper disable MemberCanBePrivate.Global

namespace Smokey.Extensions.WebDriver
{
    public static class InteractionsWebDriverExtensions
    {
        public static IWebDriver MoveToElement(
            this IWebDriver webDriver,
            IWebElement webElement)
        {
            return webDriver.Run(Interactions.MoveToElement(webDriver, webElement, moveToElement: true));
        }

        public static IWebDriver MoveToElement(
            this IWebDriver webDriver,
            string cssSelector)
        {
            return webDriver.MoveToElement(Interactions.WaitWebElement(webDriver, cssSelector));
        }

        public static IWebDriver Click(
            this IWebDriver webDriver,
            IWebElement webElement,
            bool moveToElement = true)
        {
            return webDriver
                .Run(Interactions.MoveToElement(webDriver, webElement, moveToElement))
                .Run(Interactions.Click(webDriver, webElement));
        }

        public static IWebDriver Click(
            this IWebDriver webDriver,
            string cssSelector,
            bool moveToElement = true)
        {
            return webDriver.Click(
                webElement: Interactions.WaitWebElement(webDriver, cssSelector),
                moveToElement: moveToElement);
        }

        public static IWebDriver DoubleClick(
            this IWebDriver webDriver,
            IWebElement webElement,
            bool moveToElement = true)
        {
            return webDriver
                .Run(Interactions.MoveToElement(webDriver, webElement, moveToElement))
                .Run(Interactions.DoubleClick(webDriver, webElement));
        }

        public static IWebDriver DoubleClick(
            this IWebDriver webDriver,
            string cssSelector,
            bool moveToElement = true)
        {
            return webDriver.DoubleClick(
                webElement: Interactions.WaitWebElement(webDriver, cssSelector),
                moveToElement: moveToElement);
        }

        public static IWebDriver ContextClick(
            this IWebDriver webDriver,
            IWebElement webElement,
            bool moveToElement = true)
        {
            return webDriver
                .Run(Interactions.MoveToElement(webDriver, webElement, moveToElement))
                .Run(Interactions.ContextClick(webDriver, webElement));
        }

        public static IWebDriver ContextClick(
            this IWebDriver webDriver,
            string cssSelector,
            bool moveToElement = true)
        {
            return webDriver.ContextClick(
                webElement: Interactions.WaitWebElement(webDriver, cssSelector),
                moveToElement: moveToElement);
        }

        public static IWebDriver Clear(
            this IWebDriver webDriver,
            IWebElement webElement)
        {
            return webDriver.Run(webElement.Clear);
        }

        public static IWebDriver Clear(
            this IWebDriver webDriver,
            string cssSelector)
        {
            return webDriver.Clear(Interactions.WaitWebElement(webDriver, cssSelector));
        }

        public static IWebDriver ClearLikeHuman(
            this IWebDriver webDriver,
            IWebElement webElement)
        {
            return webDriver.Run(Interactions.ClearLikeHuman(webElement));
        }

        public static IWebDriver ClearLikeHuman(
            this IWebDriver webDriver,
            string cssSelector)
        {
            return webDriver.ClearLikeHuman(Interactions.WaitWebElement(webDriver, cssSelector));
        }

        public static IWebDriver SendKeys(
            this IWebDriver webDriver,
            IWebElement webElement,
            string text)
        {
            return webDriver.Send(webElement, text);
        }

        public static IWebDriver SendKeys(
            this IWebDriver webDriver,
            string cssSelector,
            string text)
        {
            return webDriver.Send(
                webElement: Interactions.WaitWebElement(webDriver, cssSelector),
                text: text);
        }

        public static IWebDriver DragAndDrop(
            this IWebDriver webDriver,
            IWebElement from,
            IWebElement to)
        {
            return webDriver.Run(Interactions.DragAndDrop(
                webDriver: webDriver,
                from: from, 
                to: to));
        }
        
        public static IWebDriver DragAndDrop(
            this IWebDriver webDriver,
            string from,
            IWebElement to)
        {
            return webDriver.Run(Interactions.DragAndDrop(
                webDriver: webDriver,
                from: Interactions.WaitWebElement(webDriver, from), 
                to: to));
        }

        public static IWebDriver DragAndDrop(
            this IWebDriver webDriver,
            IWebElement from,
            string to)
        {
            return webDriver.Run(Interactions.DragAndDrop(
                webDriver: webDriver,
                from: from,
                to: Interactions.WaitWebElement(webDriver, to)));
        }
        
        public static IWebDriver DragAndDrop(
            this IWebDriver webDriver,
            string from,
            string to)
        {
            return webDriver.Run(Interactions.DragAndDrop(
                webDriver: webDriver,
                from: Interactions.WaitWebElement(webDriver, from),
                to: Interactions.WaitWebElement(webDriver, to)));
        }

        private static IWebDriver Send(
            this IWebDriver webDriver,
            IWebElement webElement,
            string text)
        {
            return webDriver
                .Click(webElement)
                .Clear(webElement)
                .Run(() => webElement.SendKeys(text.NotNull(Validation.Null(nameof(text)))));
        }
    }
}
