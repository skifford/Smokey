using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using Smokey.Extensions.Exceptions;

namespace Smokey.Extensions.WebDriver
{
    public static class ActionWebDriverExtensions
    {
        public static IWebDriver Run(this IWebDriver webDriver, Action action)
        {
            Guard.NotNull(action, ExceptionMessages.Validation.Null(nameof(action))).Invoke();
            return webDriver;
        }

        public static IWebDriver Run<T>(this IWebDriver webDriver, Action<T> action, T arg)
        {
            Guard.NotNull(action, ExceptionMessages.Validation.Null(nameof(action))).Invoke(arg);
            return webDriver;
        }

        public static IWebDriver Run<T>(this IWebDriver webDriver, Action<IEnumerable<T>> action, params T[] args)
        {
            Guard.NotNull(action, ExceptionMessages.Validation.Null(nameof(action))).Invoke(args);
            return webDriver;
        }
    }
}
