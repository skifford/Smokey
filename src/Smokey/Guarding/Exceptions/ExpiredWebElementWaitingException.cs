using System;

namespace Smokey.Guarding.Exceptions
{
    public sealed class ExpiredWebElementWaitingException : Exception
    {
        public ExpiredWebElementWaitingException() : base(ExceptionMessages.Selenium.Timeout())
        {
        }

        public ExpiredWebElementWaitingException(string cssSelector) : base(
            ExceptionMessages.Selenium.Timeout(cssSelector))
        {
        }
    }
}
