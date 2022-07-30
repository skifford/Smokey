using Smokey.Guarding;
using Smokey.Guarding.ExceptionMessages;

namespace Smokey.Extensions
{
    public static class StringExtensions
    {
        public static string NotNull(this string value, string message = null)
        {
            message = message ?? Validation.Null();
            
            return Guard.NotNull(value, message);
        }
        
        public static string NotEmpty(this string value, string message = null)
        {
            message = message ?? Validation.NullOrWhiteSpace();
            
            return Guard.NotEmpty(value, message);
        }
    }
}
