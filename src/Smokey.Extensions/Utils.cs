using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Smokey.Extensions
{
    internal static class Utils
    {
        public static TResult Wait<TSource, TResult>(
            TSource source,
            Func<TSource, TResult> condition,
            int timeout,
            int pollingInterval,
            CancellationToken cancellationToken = default)
        {
            var waiter = new DefaultWait<TSource>(source)
            {
                Timeout = TimeSpan.FromMilliseconds(timeout),
                PollingInterval = TimeSpan.FromMilliseconds(pollingInterval)
            };
            waiter.IgnoreExceptionTypes(typeof(NoSuchElementException));

            return waiter.Until(condition, cancellationToken);
        }
    }
}
