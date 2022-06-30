using System;

namespace Smokey.Extensions.Exceptions
{
    public sealed class ExpiredWebElementWaitingException : Exception
    {
        public ExpiredWebElementWaitingException() : base("Web element waiting time is expired")
        {
        }

        public ExpiredWebElementWaitingException(string cssSelector) : base(
            $"Waiting time for web element with selector'{cssSelector}' is expired")
        {
        }
    }
}
