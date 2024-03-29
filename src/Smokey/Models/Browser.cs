﻿using System;
using System.Threading;
using OpenQA.Selenium;
using Smokey.Guarding;

namespace Smokey.Models
{
    /// <summary>
    /// Browser implementation
    /// </summary>
    public sealed class Browser : IBrowser
    {
        public IWebDriver WebDriver { get; }
        
        public CancellationToken CancellationToken { get; }
        
        private Browser(IWebDriver webDriver, CancellationToken cancellationToken)
        {
            WebDriver = webDriver ?? throw new ArgumentNullException(nameof(webDriver));
            CancellationToken = cancellationToken;
        }

        /// <summary>
        /// Creates new instance of Browser
        /// </summary>
        /// <param name="browserConfiguration">configuration of browser instance</param>
        /// <param name="cancellationToken">token for waiting cancellation</param>
        public static Browser CreateBrowser(
            BrowserConfiguration browserConfiguration,
            CancellationToken cancellationToken = default)
        {
            Guard.NotNull(browserConfiguration);

            var driver = DriverFactory.CreateDriver(browserConfiguration);

            return new Browser(driver, cancellationToken);
        }

        /// <summary>
        /// Disposing sources of managed/unmanaged code such as <see cref="IWebDriver"/>
        /// </summary>
        public void Dispose()
        {
            WebDriver?.Dispose();
        }

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
