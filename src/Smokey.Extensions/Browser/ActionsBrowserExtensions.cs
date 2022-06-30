using System;
using System.Collections.Generic;
using Smokey.Extensions.Exceptions;

namespace Smokey.Extensions.Browser
{
    public static class ActionsBrowserExtensions
    {
        public static IBrowser Run(this IBrowser browser, Action action)
        {
            Guard.NotNull(action, ExceptionMessages.Validation.Null(nameof(action))).Invoke();
            return browser;
        }

        public static IBrowser Run<T>(this IBrowser browser, Action<T> action, T arg)
        {
            Guard.NotNull(action, ExceptionMessages.Validation.Null(nameof(action))).Invoke(arg);
            return browser;
        }

        public static IBrowser Run<T>(this IBrowser browser, Action<IEnumerable<T>> action, params T[] args)
        {
            Guard.NotNull(action, ExceptionMessages.Validation.Null(nameof(action))).Invoke(args);
            return browser;
        }
    }
}
