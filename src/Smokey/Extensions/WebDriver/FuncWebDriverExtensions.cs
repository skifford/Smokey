using System;
using OpenQA.Selenium;
using Smokey.Guarding;
using Smokey.Guarding.Exceptions;

namespace Smokey.Extensions.WebDriver
{
    public static class FuncWebDriverExtensions
    {
        public static TResult Invoke<TResult>(this IWebDriver webDriver, Func<TResult> func)
        {
            return Guard.NotNull(func, ExceptionMessages.Validation.Null(nameof(func))).Invoke();
        }
        
        public static TResult Invoke<T,TResult>(this IWebDriver webDriver, Func<T,TResult> func, T arg)
        {
            return Guard.NotNull(func, ExceptionMessages.Validation.Null(nameof(func))).Invoke(arg);
        }
        
        public static TResult Invoke<T,TResult>(this IWebDriver webDriver, Func<T[],TResult> func, params T[] args)
        {
            return Guard.NotNull(func, ExceptionMessages.Validation.Null(nameof(func))).Invoke(args);
        }
    }
}
