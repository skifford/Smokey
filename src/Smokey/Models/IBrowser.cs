using System;
using System.Threading;
using OpenQA.Selenium;

namespace Smokey.Models
{
    /// <summary>
    /// Browser interface 
    /// </summary>
    public interface IBrowser : IDisposable, IEquatable<Browser>
    {
        /// <summary>
        /// Instance of Selenium WebDriver
        /// </summary>
        public IWebDriver WebDriver { get; }

        /// <summary>
        /// Token for waiting cancellation
        /// </summary>
        public CancellationToken CancellationToken { get; }
    }
}
