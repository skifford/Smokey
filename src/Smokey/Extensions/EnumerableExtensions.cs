using System;
using System.Collections.Generic;
using System.Linq;
using Smokey.Guarding;
using Smokey.Guarding.ExceptionMessages;

namespace Smokey.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            action = Guard.NotNull(action, Validation.Null(nameof(action)));
            source = Guard.NotNull(source, Validation.Null(nameof(source))).ToList();
            
            foreach (var element in source)
            {
                action.Invoke(element);
            }

            return source;
        }

        public static bool IsEmpty<T>(this IEnumerable<T> source)
        {
            return source.Any() is false;
        }
    }
}
