using System;
using System.Diagnostics.CodeAnalysis;

namespace Smokey.Extensions.Browser
{
    public static class WaitingBrowserExtensions
    {
        public static TResult Wait<TResult>(
            this IBrowser browser,
            [NotNull] Func<IBrowser, TResult> condition,
            int timeout = Constants.Timeouts.DefaultWaitTimeout,
            int pollingInterval = Constants.Timeouts.DefaultPollingInterval)
        {
            return Utils.Wait(
                source: browser,
                condition: condition,
                timeout: timeout,
                pollingInterval: pollingInterval,
                cancellationToken: browser.CancellationToken);
        }
    }
}
