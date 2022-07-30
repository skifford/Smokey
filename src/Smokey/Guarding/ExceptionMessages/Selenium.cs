namespace Smokey.Guarding.ExceptionMessages;

public static class Selenium
{
    public static string Timeout() => "Timeout for web element is expired";

    public static string Timeout(string cssSelector) =>
        $"Timeout for web element with selector '{cssSelector}' is expired";

    public static string NotJsExecutor(string source) =>
        $"'{source}' is not IJavaScriptExecutor";
}