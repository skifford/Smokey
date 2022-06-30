using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using Smokey.Extensions.Extensions;

namespace Smokey.Extensions.WebDriver
{
    public static class JsWebDriverExtensions
    {
        public static IWebDriver ExecuteScripts(
            this IWebDriver webDriver, 
            IReadOnlyCollection<string> scripts, 
            bool async = false)
        {
            if (scripts is null)
            {
                throw new ArgumentNullException(nameof(scripts));
            }

            if (scripts.Any() is false)
            {
                return webDriver;
            }

            scripts.ForEach(script => webDriver.ExecuteScript(script, async));

            return webDriver;
        }

        public static IWebDriver ExecuteScript(
            this IWebDriver webDriver, 
            string script, 
            bool async = false)
        {
            if (webDriver is not IJavaScriptExecutor jse)
            {
                throw new Exception($"{nameof(webDriver)} is not {nameof(IJavaScriptExecutor)}");
            }

            if (async)
            {
                jse.ExecuteAsyncScript(script.Validate());
            }
            else
            {
                jse.ExecuteScript(script.Validate());
            }

            return webDriver;
        }
    }
}
