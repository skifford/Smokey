using System;
using Smokey.Guarding.ExceptionMessages;

namespace Smokey.Guarding
{
    public static class Guard
    {
        public static T NotNull<T>(T value, string message = null)
        {
            message = string.IsNullOrWhiteSpace(message) 
                ? Validation.Null() 
                : message;
            
            if (value is null)
            {
                throw new ArgumentNullException(message);
            }

            return value;
        }

        public static string NotEmpty(string value, string message = null)
        {
            message = string.IsNullOrWhiteSpace(message) 
                ? Validation.NullOrEmpty() 
                : message;
            
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(message);
            }

            return value;
        }
    }
}
