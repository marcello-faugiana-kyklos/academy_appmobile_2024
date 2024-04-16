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
    const int MinValue = 10;
    const int MaxValue = 100;
    public static async Task<(string Place, string Code)[]> GetPlaceAndCodesFromFileAsync
    (
        this string path,
        CancellationToken cancellationToken = default,
        string? s = "hello",
        int from = MinValue,
        int to = MaxValue
    )
    {
        string[] values = await File.ReadAllLinesAsync(path, cancellationToken);
        
        cancellationToken.ThrowIfCancellationRequested();
        
        return
            values
            .Select(x => x.SplitLineIntoPlaceAndCode('|'))
            .ToArray();
    }

    public static void AssertArgumentNotNull(this object item, string paramName) =>
        ArgumentNullException.ThrowIfNull(item, paramName);

    public static T GetNonNullOrThrow<T>(this T item, string paramName) 
        where T : class 
    {
        ArgumentNullException.ThrowIfNull(item, paramName);
        return item;
    }

    public static string GetWithTextOrThrow(this string s, string paramName)
    {
        if (string.IsNullOrWhiteSpace(s))
        {
            throw new ArgumentNullException(paramName);
        }

        return s;
    }

    public static bool IsNullOrEmpty(this string? s) =>
        string.IsNullOrEmpty(s);

    public static DateOnly ToDateOnly(this DateTime date) =>
        new DateOnly(date.Year, date.Month, date.Day);
}
