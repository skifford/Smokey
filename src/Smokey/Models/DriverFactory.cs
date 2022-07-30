using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;

namespace Smokey.Models
{
    /// <summary>
    /// Factory for creating web drivers by given browser configuration
    /// </summary>
    public static class DriverFactory
    {
        private static readonly Dictionary<BrowserType, Func<IReadOnlyCollection<string>, DriverOptions>>
            OptionsByBrowserType = new()
            {
                { BrowserType.Chrome, CreateOptions<ChromeOptions> },
                { BrowserType.Firefox, CreateOptions<FirefoxOptions> },
                { BrowserType.Edge, CreateOptions<EdgeOptions> }
            };
        
        /// <summary>
        /// Creates web driver for browser by given configuration
        /// </summary>
        /// <param name="configuration">configuration of browser</param>
        /// <returns>Instance of <see cref="IWebDriver"/></returns>
        /// <exception cref="ArgumentNullException">configuration can not be null</exception>
        /// <exception cref="ArgumentOutOfRangeException">supported browser types: Chrome, Firefox and Edge</exception>
        public static IWebDriver CreateDriver(BrowserConfiguration configuration)
        {
            if (configuration is null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            if (string.IsNullOrWhiteSpace(configuration.RemoteHost) is false)
            {
                return CreateRemoteDriver(configuration);
            }

            return configuration.BrowserType switch
            {
                BrowserType.Chrome => CreateLocalChromeDriver(configuration),
                BrowserType.Firefox => CreateLocalFirefoxDriver(configuration),
                BrowserType.Edge => CreateLocalEdgeDriver(configuration),
                _ => throw new ArgumentOutOfRangeException(nameof(configuration.BrowserType))
            };
        }

        private static TOptions CreateOptions<TOptions>([NotNull] IReadOnlyCollection<string> arguments)
            where TOptions : DriverOptions, new()
        {
            TOptions options = new();
            switch (typeof(TOptions))
            {
                case var type when type == typeof(ChromeOptions):
                    (options as ChromeOptions)?.AddArguments(arguments);
                    break;
                case var type when type == typeof(FirefoxOptions):
                    (options as FirefoxOptions)?.AddArguments(arguments);
                    break;
                case var type when type == typeof(EdgeOptions):
                    (options as EdgeOptions)?.AddArguments(arguments);
                    break;
            }

            return options;
        }

        private static DriverOptions GetOptions(BrowserConfiguration configuration)
        {
            if (OptionsByBrowserType.TryGetValue(configuration.BrowserType, out var func) is false)
            {
                throw new ArgumentOutOfRangeException(nameof(configuration.BrowserType));
            }

            return func?.Invoke(configuration.Arguments);
        }

        private static IWebDriver CreateRemoteDriver(BrowserConfiguration configuration)
        {
            var uri = new Uri(configuration.RemoteHost);
            var options = GetOptions(configuration);

            return new RemoteWebDriver(uri, options);
        }

        private static IWebDriver CreateLocalChromeDriver(BrowserConfiguration configuration)
        {
            var service = ChromeDriverService.CreateDefaultService(configuration.DriverSource);
            var options = GetOptions(configuration) as ChromeOptions;

            return new ChromeDriver(service, options);
        }

        private static IWebDriver CreateLocalFirefoxDriver(BrowserConfiguration configuration)
        {
            var service = FirefoxDriverService.CreateDefaultService(configuration.DriverSource);
            var options = GetOptions(configuration) as FirefoxOptions;

            return new FirefoxDriver(service, options);
        }

        private static IWebDriver CreateLocalEdgeDriver(BrowserConfiguration configuration)
        {
            var service = EdgeDriverService.CreateDefaultService(configuration.DriverSource);
            var options = GetOptions(configuration) as EdgeOptions;

            return new EdgeDriver(service, options);
        }
    }
}
