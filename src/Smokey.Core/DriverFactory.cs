using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using OpenQA.Selenium.Edge;

namespace Smokey
{
    public sealed class DriverFactory
    {
        private readonly ILogger _logger;

        public DriverFactory(ILogger logger)
        {
            _logger = logger;
        }
        
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="NotImplementedException"></exception>
        public IWebDriver CreateDriver(BrowserConfiguration configuration)
        {
            if (configuration is null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }
            
            return configuration.BrowserType switch
            {
                BrowserType.Remote => CreateRemoteDriver(configuration),
                BrowserType.Chrome => CreateChromeDriver(configuration),
                BrowserType.Firefox => CreateFirefoxDriver(configuration),
                BrowserType.Edge => CreateEdgeDriver(configuration),
                _ => throw new NotImplementedException(nameof(configuration.BrowserType)),
            };
        }

        private IWebDriver CreateRemoteDriver(BrowserConfiguration configuration)
        {
            Uri uri;
            try
            {
                uri = new Uri(configuration.RemoteHost);
            }
            catch(ArgumentNullException)
            {
                _logger?.LogError($"Remote driver creation failed.\n" +
                    $"{nameof(configuration.RemoteHost)} is null.\n");
                throw;
            }
            catch (UriFormatException)
            {
                _logger?.LogError($"Remote driver creation failed.\n" +
                    $"{nameof(configuration.RemoteHost)} has invalid uri format: " +
                    $"{configuration.RemoteHost}\n");
                throw;
            }
            var options = new ChromeOptions();

            return new RemoteWebDriver(uri, options);
        }

        private IWebDriver CreateChromeDriver(BrowserConfiguration configuration)
        {
            var service = ChromeDriverService.CreateDefaultService();
            try
            {
                service = ChromeDriverService.CreateDefaultService(configuration.DriverSource);
            }
            catch (Exception e)
            {
                _logger?.LogError($"Chrome driver creation failed.\n" +
                    $"{e.Message}\n" +
                    $"{e.StackTrace}");
                service?.Dispose();
                throw;
            }
            var options = CreateOptions<ChromeOptions>(configuration.Arguments);

            return new ChromeDriver(service, options);
        }

        private IWebDriver CreateFirefoxDriver(BrowserConfiguration configuration)
        {
            var service = FirefoxDriverService.CreateDefaultService();
            try
            {
                service = FirefoxDriverService.CreateDefaultService(configuration.DriverSource);
            }
            catch (Exception e)
            {
                _logger?.LogError($"Firefox driver creation failed.\n" +
                                  $"{e.Message}\n" +
                                  $"{e.StackTrace}");
                service?.Dispose();
                throw;
            }
            var options = CreateOptions<FirefoxOptions>(configuration.Arguments);

            return new FirefoxDriver(service, options);
        }
        
        private IWebDriver CreateEdgeDriver(BrowserConfiguration configuration)
        {
            var service = EdgeDriverService.CreateDefaultService();
            try
            {
                service = EdgeDriverService.CreateDefaultService(configuration.DriverSource);
            }
            catch (Exception e)
            {
                _logger?.LogError($"Edge driver creation failed.\n" +
                                  $"{e.Message}\n" +
                                  $"{e.StackTrace}");
                service?.Dispose();
                throw;
            }
            var options = CreateOptions<EdgeOptions>(configuration.Arguments);

            return new EdgeDriver(service, options);
        }

        private static TOptions CreateOptions<TOptions>(IReadOnlyCollection<string> arguments) 
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
    }
}
