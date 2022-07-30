using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using Smokey.Guarding;
using Smokey.Guarding.ExceptionMessages;

namespace Smokey.Extensions.WebDriver
{
    public static class JsWebDriverExtensions
    {
        public static IWebDriver ExecuteScripts(
            this IWebDriver webDriver,
            IReadOnlyCollection<string> scripts,
            bool async = false)
        {
            scripts = Guard.NotNull(scripts, Validation.Null(nameof(scripts)));

            if (scripts.IsEmpty())
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
                throw new InvalidCastException(Selenium.NotJsExecutor(nameof(webDriver)));
            }
            
            Guard.NotEmpty(script, Validation.NullOrWhiteSpace(nameof(script)));

            if (async)
            {
                jse.ExecuteAsyncScript(script);
            }
            else
            {
                jse.ExecuteScript(script);
            }

            return webDriver;
        }
    }
}
