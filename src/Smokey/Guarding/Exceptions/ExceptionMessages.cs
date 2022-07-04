namespace Smokey.Guarding.Exceptions
{
    public static class ExceptionMessages
    {
        public static class Validation
        {
            public static string Null() => "Value can not be null";
            public static string Null(string value) => $"Value '{value}' can not be null";
            public static string NullOrEmpty() => "Value can not be null or empty";
            public static string NullOrWhiteSpace() => "Value can not be null or empty or white space";

            public static string NullOrWhiteSpace(string value) =>
                $"Value '{value}' can not be null or empty or white space";
        }

        public static class Selenium
        {
            public static string Timeout() => "Timeout for web element is expired";

            public static string Timeout(string cssSelector) =>
                $"Timeout for web element with selector '{cssSelector}' is expired";

            public static string NotJsExecutor(string source) =>
                $"'{source}' is not IJavaScriptExecutor";
        }
    }
}
