using System;
using System.Collections.Generic;

namespace Smokey.Extensions.Browser
{
    public static class ActionsBrowserExtensions
    {
        public static IBrowser Run(this IBrowser browser, Action action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            action.Invoke();
            
            return browser;
        }

        public static IBrowser Run<T>(this IBrowser browser, Action<T> action, T arg)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            
            action.Invoke(arg);
            
            return browser;
        }

        public static IBrowser Run<T>(this IBrowser browser, Action<IEnumerable<T>> action, params T[] args)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            
            action.Invoke(args);
            
            return browser;
        }
    }
}
