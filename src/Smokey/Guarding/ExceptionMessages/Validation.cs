namespace Smokey.Guarding.ExceptionMessages;

public static class Validation
{
    public static string Null() => "Value can not be null";
    public static string Null(string value) => $"Value '{value}' can not be null";
    public static string NullOrEmpty() => "Value can not be null or empty";
    public static string NullOrWhiteSpace() => "Value can not be null or empty or white space";

    public static string NullOrWhiteSpace(string value) =>
        $"Value '{value}' can not be null or empty or white space";
}