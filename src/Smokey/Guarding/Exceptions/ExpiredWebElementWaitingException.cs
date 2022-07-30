using System;
using Smokey.Guarding.ExceptionMessages;

namespace Smokey.Guarding.Exceptions
{
    public sealed class ExpiredWebElementWaitingException : Exception
    {
        public ExpiredWebElementWaitingException() : base(Selenium.Timeout())
        {
        }

        public ExpiredWebElementWaitingException(string cssSelector) : base(Selenium.Timeout(cssSelector))
        {
        }
    }
}
