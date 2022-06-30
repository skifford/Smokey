using System;
using System.Threading;
using OpenQA.Selenium;

namespace Smokey
{
    public interface IBrowser : IDisposable, IEquatable<Browser>
    {
        public IWebDriver WebDriver { get; }

        public CancellationToken CancellationToken { get; }
    }
}
