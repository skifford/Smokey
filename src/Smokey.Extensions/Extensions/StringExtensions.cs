using System;

namespace Smokey.Extensions.Extensions
{
    internal static class StringExtensions
    {
        public static string Validate(this string cssSelector)
        {
            if (string.IsNullOrWhiteSpace(cssSelector))
            {
                throw new ArgumentNullException(nameof(cssSelector));
            }

            return cssSelector;
        }
    }
}
