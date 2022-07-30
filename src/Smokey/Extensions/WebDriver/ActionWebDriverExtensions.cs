using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using Smokey.Guarding;
using Smokey.Guarding.ExceptionMessages;

namespace Smokey.Extensions.WebDriver
{
    public static class ActionWebDriverExtensions
    {
        public static IWebDriver Run(this IWebDriver webDriver, Action action)
        {
            Guard.NotNull(action, Validation.Null(nameof(action)));

            action.Invoke();

            return webDriver;
        }

        public static IWebDriver Run<T>(this IWebDriver webDriver, Action<T> action, T arg)
        {
            Guard.NotNull(action, Validation.Null(nameof(action)));

            action.Invoke(arg);

            return webDriver;
        }

        public static IWebDriver Run<T>(this IWebDriver webDriver, Action<IEnumerable<T>> action, params T[] args)
        {
            Guard.NotNull(action, Validation.Null(nameof(action)));
            
            action.Invoke(args);
            
            return webDriver;
        }
    }
}