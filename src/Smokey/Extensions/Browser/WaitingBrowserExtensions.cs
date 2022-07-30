using System;
using Smokey.Constants;
using Smokey.Features.Waiting;
using Smokey.Models;

namespace Smokey.Extensions.Browser
{
    public static class WaitingBrowserExtensions
    {
        public static TResult Wait<TResult>(
            this IBrowser browser,
            Func<IBrowser, TResult> condition,
            int timeout = Timeouts.DefaultWaitTimeout,
            int pollingInterval = Timeouts.DefaultPollingInterval)
        {
            return Waiter.Wait(
                source: browser,
                condition: condition,
                timeout: timeout,
                pollingInterval: pollingInterval,
                cancellationToken: browser.CancellationToken);
        }
    }
}
