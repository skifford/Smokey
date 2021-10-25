using System;

namespace Smokey.Demo.MSTest.DI.Logger
{
    public static class StringExtensions
    {
        public static string WithDateTime(this string message)
        {
            return $"{DateTime.Now} {message}";
        }
    }
}