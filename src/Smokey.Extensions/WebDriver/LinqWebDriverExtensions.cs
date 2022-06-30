using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace Smokey.Extensions.WebDriver
{
    public static class LinqWebDriverExtensions
    {
        public static IEnumerable<TResult> Select<TResult>(
            this IWebDriver webDriver, 
            string cssSelector, 
            Func<IWebElement, TResult> selector)
        {
            return webDriver.WaitWebElements(cssSelector).Select(selector);
        }
        
        public static IEnumerable<IWebElement> Where(
            this IWebDriver webDriver, 
            string cssSelector, 
            Func<IWebElement, bool> predicate)
        {
            return webDriver.WaitWebElements(cssSelector).Where(predicate);
        }
        
        public static IWebElement First(
            this IWebDriver webDriver, 
            string cssSelector, 
            Func<IWebElement, bool> predicate = null)
        {
            return webDriver.WaitWebElements(cssSelector).First(predicate ?? (_ => true));
        }
        
        public static IWebElement FirstOrDefault(
            this IWebDriver webDriver, 
            string cssSelector, 
            Func<IWebElement, bool> predicate = null)
        {
            return webDriver.WaitWebElements(cssSelector).FirstOrDefault(predicate ?? (_ => true));
        }
        
        public static IWebElement Single(
            this IWebDriver webDriver, 
            string cssSelector, 
            Func<IWebElement, bool> predicate = null)
        {
            return webDriver.WaitWebElements(cssSelector).Single(predicate ?? (_ => true));
        }
        
        public static IWebElement SingleOrDefault(
            this IWebDriver webDriver, 
            string cssSelector, 
            Func<IWebElement, bool> predicate = null)
        {
            return webDriver.WaitWebElements(cssSelector).SingleOrDefault(predicate ?? (_ => true));
        }
        
        public static bool All(
            this IWebDriver webDriver, 
            string cssSelector, 
            Func<IWebElement, bool> predicate = null)
        {
            return webDriver.WaitWebElements(cssSelector).All(predicate ?? (_ => true));
        }
        
        public static bool Any(
            this IWebDriver webDriver, 
            string cssSelector, 
            Func<IWebElement, bool> predicate = null)
        {
            return webDriver.WaitWebElements(cssSelector).Any(predicate ?? (_ => true));
        }
        
        public static int Count(
            this IWebDriver webDriver, 
            string cssSelector,
            Func<IWebElement, bool> predicate = null)
        {
            return webDriver.WaitWebElements(cssSelector).Count(predicate ?? (_ => true));
        }
    }
}
