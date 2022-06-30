using System;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace Smokey.Extensions.WebDriver
{
    public static class ActionWebDriverExtensions
    {
        public static IWebDriver Run(this IWebDriver webDriver, Action action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            action.Invoke();
            
            return webDriver;
        }

        public static IWebDriver Run<T>(this IWebDriver webDriver, Action<T> action, T arg)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            
            action.Invoke(arg);
            
            return webDriver;
        }

        public static IWebDriver Run<T>(this IWebDriver webDriver, Action<IEnumerable<T>> action, params T[] args)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            
            action.Invoke(args);
            
            return webDriver;
        }
    }
}
