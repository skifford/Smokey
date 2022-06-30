using System;
using Smokey.Extensions.Exceptions;

namespace Smokey.Extensions
{
    public static class Guard
    {
        public static T NotNull<T>(T value, string message)
        {
            message = string.IsNullOrWhiteSpace(message) 
                ? ExceptionMessages.Validation.Null() 
                : message;
            
            if (value is null)
            {
                throw new ArgumentNullException(message);
            }

            return value;
        }

        public static string NotEmpty(string value, string message)
        {
            message = string.IsNullOrWhiteSpace(message) 
                ? ExceptionMessages.Validation.NullOrEmpty() 
                : message;
            
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(message);
            }

            return value;
        }
    }
}
