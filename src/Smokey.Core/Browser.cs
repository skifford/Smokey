using System;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;

namespace Smokey
{
    public sealed class Browser : IDisposable, IEquatable<Browser>
    {
        private bool _isDisposed;

        public IWebDriver WebDriver { get; }
        
        /// <exception cref="ArgumentNullException"></exception>
        public Browser(IWebDriver webDriver)
        {
            WebDriver = webDriver ?? throw new ArgumentNullException(nameof(webDriver));
        }
        
        /// <exception cref="ArgumentNullException"></exception>
        public static Browser CreateBrowser(BrowserConfiguration browserConfiguration, ILogger logger = null)
        {
            if (browserConfiguration is null)
            {
                throw new ArgumentNullException(nameof(browserConfiguration));
            }
            
            var driverFactory = new DriverFactory(logger);
            var driver = driverFactory.CreateDriver(browserConfiguration);
            return new Browser(driver);
        }

        #region IDisposable

        private void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }

            WebDriver?.Quit();
            WebDriver?.Dispose();
            _isDisposed = true;
        }

        ~Browser()
        {
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion IDisposable

        #region IEquatable<Browser>

        public bool Equals(Browser other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(WebDriver, other.WebDriver);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is Browser other && Equals(other);
        }

        public static bool operator ==(Browser left, Browser right)
        {
            return left is not null && left.Equals(right);
        }

        public static bool operator !=(Browser left, Browser right)
        {
            return !(left == right);
        }

        public override int GetHashCode()
        {
            return (WebDriver != null ? WebDriver.GetHashCode() : 0);
        }

        #endregion IEquatable<Browser>
    }
}
