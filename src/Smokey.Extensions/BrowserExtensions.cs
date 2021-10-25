using OpenQA.Selenium;
using System.Linq;
using OpenQA.Selenium.Html5;
using Smokey.Extensions.Models;

namespace Smokey.Extensions
{
    public static class BrowserExtensions
    {
        public static void NewTab(this Browser browser, string url)
        {
            var driver = browser.WebDriver;
            (driver as IJavaScriptExecutor)?.ExecuteScript("window.open();");
            driver.SwitchTo()
                  .Window(driver.WindowHandles.Last())
                  .Navigate()
                  .GoToUrl(url);
        }
        
        public static  void SetTokens(this Browser browser, TokenView token)
        {
            if (browser.WebDriver is not IHasWebStorage hasWebStorage)
            {
                return;
            }
            
            if (hasWebStorage.HasWebStorage)
            {
                var webStorage = hasWebStorage.WebStorage;
                webStorage.LocalStorage.SetItem("access_token", token.AccessToken);
                webStorage.LocalStorage.SetItem("refresh_token", token.RefreshToken);
                webStorage.SessionStorage.SetItem("access_token", token.AccessToken);
                webStorage.SessionStorage.SetItem("refresh_token", token.RefreshToken);
            }
            else
            {
                var jsExecutor = (IJavaScriptExecutor)browser.WebDriver;
                jsExecutor.ExecuteScript($"localStorage.setItem('access_token', '{token.AccessToken}');");
                jsExecutor.ExecuteScript($"localStorage.setItem('refresh_token', '{token.RefreshToken}');");
                jsExecutor.ExecuteScript($"sessionStorage.setItem('access_token', '{token.AccessToken}');");
                jsExecutor.ExecuteScript($"sessionStorage.setItem('refresh_token', '{token.RefreshToken}');");
            }
        }
        
        public static void RemoveTokens(this Browser browser)
        {
            if (browser.WebDriver is not IHasWebStorage hasWebStorage)
            {
                return;
            }

            if (hasWebStorage.HasWebStorage)
            {
                var webStorage = hasWebStorage.WebStorage;
                webStorage.LocalStorage.RemoveItem("access_token");
                webStorage.LocalStorage.RemoveItem("refresh_token");
                webStorage.SessionStorage.RemoveItem("access_token");
                webStorage.SessionStorage.RemoveItem("refresh_token");
            }
            else
            {
                if (browser.WebDriver is not IJavaScriptExecutor jsExecutor)
                {
                    return;
                }
                
                jsExecutor.ExecuteScript($"localStorage.removeItem('access_token');");
                jsExecutor.ExecuteScript($"localStorage.removeItem('refresh_token');");
                jsExecutor.ExecuteScript($"sessionStorage.removeItem('access_token');");
                jsExecutor.ExecuteScript($"sessionStorage.removeItem('refresh_token');");
            }
        }
    }
}
