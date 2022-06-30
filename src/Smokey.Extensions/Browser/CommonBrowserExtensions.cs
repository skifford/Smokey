using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Html5;
using Smokey.Extensions.Exceptions;
using Smokey.Extensions.Extensions;
using Smokey.Extensions.Models;
using Smokey.Extensions.WebDriver;

// ReSharper disable SuspiciousTypeConversion.Global

namespace Smokey.Extensions.Browser
{
    public static class CommonBrowserExtensions
    {
        public static IBrowser NewTab(this IBrowser browser, string url)
        {
            browser
                .WebDriver
                .ExecuteScript("window.open();")
                .SwitchTo()
                .Window(browser.WebDriver.WindowHandles.Last())
                .Navigate()
                .GoToUrl(url.NotEmpty(ExceptionMessages.Validation.NullOrWhiteSpace(nameof(url))));

            return browser;
        }

        public static IBrowser SetTokens(this IBrowser browser, TokenView token)
        {
            if (browser.WebDriver is not IHasWebStorage hasWebStorage)
            {
                return browser;
            }

            if (hasWebStorage.HasWebStorage)
            {
                var webStorage = hasWebStorage.WebStorage;
                webStorage?.LocalStorage?.SetItem("access_token", token.AccessToken);
                webStorage?.LocalStorage?.SetItem("refresh_token", token.RefreshToken);
                webStorage?.SessionStorage?.SetItem("access_token", token.AccessToken);
                webStorage?.SessionStorage?.SetItem("refresh_token", token.RefreshToken);
            }
            else
            {
                browser.WebDriver.ExecuteScripts(new[]
                {
                    "localStorage.setItem('access_token');",
                    "localStorage.setItem('refresh_token');",
                    "sessionStorage.setItem('access_token');",
                    "sessionStorage.setItem('refresh_token');"
                });
            }

            return browser;
        }

        public static IBrowser RemoveTokens(this IBrowser browser)
        {
            if (browser.WebDriver is IHasWebStorage storage)
            {
                storage.WebStorage?.SessionStorage?.RemoveItem("access_token");
                storage.WebStorage?.SessionStorage?.RemoveItem("refresh_token");
                storage.WebStorage?.LocalStorage?.RemoveItem("access_token");
                storage.WebStorage?.LocalStorage?.RemoveItem("refresh_token");
            }
            else
            {
                if (browser.WebDriver is not IJavaScriptExecutor)
                    return browser;

                browser.WebDriver.ExecuteScripts(new[]
                {
                    "localStorage.removeItem('access_token');",
                    "localStorage.removeItem('refresh_token');",
                    "sessionStorage.removeItem('access_token');",
                    "sessionStorage.removeItem('refresh_token');"
                });
            }

            return browser;
        }
    }
}
