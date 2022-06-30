using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using OpenQA.Selenium;
using Smokey.Extensions.Extensions;

namespace Smokey.Extensions.WebDriver
{
    public static class WaitingWebDriverExtensions
    {
        public static TResult Wait<TResult>(
            this IWebDriver webDriver,
            [NotNull] Func<IWebDriver, TResult> condition,
            int timeout = Constants.Timeouts.DefaultWaitTimeout,
            int pollingInterval = Constants.Timeouts.DefaultPollingInterval,
            CancellationToken cancellationToken = default)
        {
            return Utils.Wait(
                source: webDriver,
                condition: condition,
                timeout: timeout,
                pollingInterval: pollingInterval,
                cancellationToken: cancellationToken);
        }

        public static IWebElement WaitWebElement(this IWebDriver webDriver, string cssSelector)
        {
            return webDriver.Wait(driver => driver.FindElement(By.CssSelector(cssSelector)));
        }

        public static ReadOnlyCollection<IWebElement> WaitWebElements(this IWebDriver webDriver, string cssSelector)
        {
            return webDriver.Wait(driver => driver.FindElements(By.CssSelector(cssSelector)));
        }

        public static IWebElement WaitWebElement(
            this IWebDriver webDriver,
            IWebElement parent,
            string cssSelector)
        {
            webDriver.WaitWebElement(cssSelector.Validate());
            return parent.FindElement(By.CssSelector(cssSelector));
        }

        public static ReadOnlyCollection<IWebElement> WaitWebElements(
            this IWebDriver webDriver,
            IWebElement parent,
            string cssSelector)
        {
            webDriver.WaitWebElement(cssSelector.Validate());
            return parent.FindElements(By.CssSelector(cssSelector));
        }

        public static bool WaitToAppear(
            this IWebDriver webDriver,
            string cssSelector,
            int timeout = Constants.Timeouts.DefaultWaitTimeout,
            int pollingInterval = Constants.Timeouts.DefaultPollingInterval)
        {
            return webDriver.Wait(driver => driver.Exist(cssSelector.Validate()), timeout, pollingInterval);
        }

        public static bool WaitToDisappear(
            this IWebDriver webDriver,
            string cssSelector,
            int timeout = Constants.Timeouts.DefaultWaitTimeout,
            int pollingInterval = Constants.Timeouts.DefaultPollingInterval)
        {
            return webDriver.Wait(driver => driver.Exist(cssSelector.Validate()) is false, timeout, pollingInterval);
        }
    }
}
