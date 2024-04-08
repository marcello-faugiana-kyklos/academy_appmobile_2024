namespace OOPClassLibrary.Support;

public static class ExtensionMethods
{
    public static (string Place, string Code) SplitLineIntoPlaceAndCode
    (
        this string line,
        char splitChar = '@'
    )
    {
        var parts = line.Split(splitChar);
        return (parts[0].Trim(), parts[1].Trim());
    }
}
