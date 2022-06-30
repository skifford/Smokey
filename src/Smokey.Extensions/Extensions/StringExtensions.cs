using Smokey.Extensions.Exceptions;

namespace Smokey.Extensions.Extensions
{
    internal static class StringExtensions
    {
        public static string NotNull(this string value, string message = null)
        {
            message ??= ExceptionMessages.Validation.Null();
            
            return Guard.NotNull(value, message);
        }
        
        public static string NotEmpty(this string value, string message = null)
        {
            message ??= ExceptionMessages.Validation.NullOrWhiteSpace();
            
            return Guard.NotEmpty(value, message);
        }
    }
}
