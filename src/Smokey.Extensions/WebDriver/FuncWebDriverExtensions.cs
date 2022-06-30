using System;
using OpenQA.Selenium;

namespace Smokey.Extensions.WebDriver
{
    public static class FuncWebDriverExtensions
    {
        public static TResult Invoke<TResult>(this IWebDriver webDriver, Func<TResult> func)
        {
            if (func is null)
            {
                throw new ArgumentNullException(nameof(func));
            }

            return func.Invoke();
        }
        
        public static TResult Invoke<T,TResult>(this IWebDriver webDriver, Func<T,TResult> func, T arg)
        {
            if (func is null)
            {
                throw new ArgumentNullException(nameof(func));
            }

            return func.Invoke(arg);
        }
        
        public static TResult Invoke<T,TResult>(this IWebDriver webDriver, Func<T[],TResult> func, params T[] args)
        {
            if (func is null)
            {
                throw new ArgumentNullException(nameof(func));
            }

            return func.Invoke(args);
        }
    }
}
