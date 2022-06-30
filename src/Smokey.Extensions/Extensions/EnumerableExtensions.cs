using System;
using System.Collections.Generic;

namespace Smokey.Extensions.Extensions
{
    internal static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            
            foreach (var element in source)
            {
                action.Invoke(element);
            }
        }
    }
}
