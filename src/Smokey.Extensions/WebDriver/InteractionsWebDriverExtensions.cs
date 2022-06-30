using System;
using OpenQA.Selenium;
using Smokey.Extensions.Extensions;

// ReSharper disable UnusedMethodReturnValue.Local
// ReSharper disable MemberCanBePrivate.Global

namespace Smokey.Extensions.WebDriver
{
    public static class InteractionsWebDriverExtensions
    {
        public static IWebDriver MoveToElement(
            this IWebDriver webDriver,
            IWebElement webElement)
        {
            return webDriver.Run(InteractionsFactory.MoveToElement(webDriver, webElement, moveToElement: true));
        }

        public static IWebDriver MoveToElement(
            this IWebDriver webDriver,
            string cssSelector)
        {
            return webDriver.MoveToElement(InteractionsFactory.WaitWebElement(webDriver, cssSelector));
        }

        public static IWebDriver Click(
            this IWebDriver webDriver,
            IWebElement webElement,
            bool moveToElement = true)
        {
            return webDriver.TryClick(webElement, moveToElement);
        }

        public static IWebDriver Click(
            this IWebDriver webDriver,
            string cssSelector,
            bool moveToElement = true)
        {
            return webDriver.TryClick(cssSelector, moveToElement);
        }

        public static IWebDriver DoubleClick(
            this IWebDriver webDriver,
            IWebElement webElement,
            bool moveToElement = true)
        {
            return webDriver
                .Run(InteractionsFactory.MoveToElement(webDriver, webElement, moveToElement))
                .Run(InteractionsFactory.DoubleClick(webDriver, webElement));
        }

        public static IWebDriver DoubleClick(
            this IWebDriver webDriver,
            string cssSelector,
            bool moveToElement = true)
        {
            return webDriver.DoubleClick(
                webElement: InteractionsFactory.WaitWebElement(webDriver, cssSelector),
                moveToElement: moveToElement);
        }

        public static IWebDriver ContextClick(
            this IWebDriver webDriver,
            IWebElement webElement,
            bool moveToElement = true)
        {
            return webDriver
                .Run(InteractionsFactory.MoveToElement(webDriver, webElement, moveToElement))
                .Run(InteractionsFactory.ContextClick(webDriver, webElement));
        }

        public static IWebDriver ContextClick(
            this IWebDriver webDriver,
            string cssSelector,
            bool moveToElement = true)
        {
            return webDriver.ContextClick(
                webElement: InteractionsFactory.WaitWebElement(webDriver, cssSelector),
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
            return webDriver.Clear(InteractionsFactory.WaitWebElement(webDriver, cssSelector));
        }

        public static IWebDriver ClearLikeHuman(
            this IWebDriver webDriver,
            IWebElement webElement)
        {
            return webDriver.Run(() =>
            {
                for (var i = 0; i < 3; i++)
                {
                    webElement.SendKeys(Keys.Control + 'A' + Keys.Backspace);
                }
            });
        }

        public static IWebDriver ClearLikeHuman(
            this IWebDriver webDriver,
            string cssSelector)
        {
            return webDriver.ClearLikeHuman(InteractionsFactory.WaitWebElement(webDriver, cssSelector));
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
                webElement: InteractionsFactory.WaitWebElement(webDriver, cssSelector),
                text: text);
        }

        public static IWebDriver DragAndDrop(
            this IWebDriver webDriver,
            IWebElement from,
            IWebElement to)
        {
            return webDriver.Run(InteractionsFactory.DragAndDrop(
                webDriver: webDriver,
                from: from, 
                to: to));
        }
        
        public static IWebDriver DragAndDrop(
            this IWebDriver webDriver,
            string from,
            IWebElement to)
        {
            return webDriver.Run(InteractionsFactory.DragAndDrop(
                webDriver: webDriver,
                from: InteractionsFactory.WaitWebElement(webDriver, from), 
                to: to));
        }

        public static IWebDriver DragAndDrop(
            this IWebDriver webDriver,
            IWebElement from,
            string to)
        {
            return webDriver.Run(InteractionsFactory.DragAndDrop(
                webDriver: webDriver,
                from: from,
                to: InteractionsFactory.WaitWebElement(webDriver, to)));
        }
        
        public static IWebDriver DragAndDrop(
            this IWebDriver webDriver,
            string from,
            string to)
        {
            return webDriver.Run(InteractionsFactory.DragAndDrop(
                webDriver: webDriver,
                from: InteractionsFactory.WaitWebElement(webDriver, from),
                to: InteractionsFactory.WaitWebElement(webDriver, to)));
        }

        private static IWebDriver TryClick(
            this IWebDriver webDriver,
            IWebElement webElement,
            bool moveToElement = true)
        {
            Exception ClickWithException()
            {
                if (moveToElement)
                {
                    webDriver.MoveToElement(webElement);
                }

                try
                {
                    webElement.Click();
                    return null;
                }
                catch (Exception exception)
                {
                    return exception;
                }
            }

            var exception = webDriver.Wait(condition: _ => ClickWithException());

            if (exception is not null)
            {
                throw exception;
            }

            return webDriver;
        }

        private static IWebDriver TryClick(
            this IWebDriver webDriver,
            string cssSelector,
            bool moveToElement = true)
        {
            return webDriver.TryClick(
                webElement: InteractionsFactory.WaitWebElement(webDriver, cssSelector),
                moveToElement: moveToElement);
        }

        private static IWebDriver Send(
            this IWebDriver webDriver,
            IWebElement webElement,
            string text)
        {
            return webDriver
                .Click(webElement)
                .Clear(webElement)
                .Run(() => webElement.SendKeys(text.Validate()));
        }
    }
}