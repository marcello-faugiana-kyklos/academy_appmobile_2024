using OOPClassLibrary.Support;

namespace OOPClassLibrary.Fiscal.IFaceMethod;

public class PlaceOfBirthCodeRetrieverByDictionary : 
    IPlaceOfBirthCodeRetriever
{
    private static readonly Dictionary<string, string> belfioreCodes;

    static PlaceOfBirthCodeRetrieverByDictionary()
    {
        belfioreCodes =
            File
            .ReadLines(@"./Data/BelfioreCodes.txt")
            .Select(line => line.SplitLineIntoPlaceAndCode('|'))
            .ToDictionary
            (
                x => x.Place,
                x => x.Code,
                StringComparer.InvariantCultureIgnoreCase
            );
    }

    public string GetPlaceOfBirthCode(string placeOfBirth)
    {
        if (belfioreCodes.TryGetValue(placeOfBirth, out string? code))
        {
            return code;
        }

        throw new Exception($"'{placeOfBirth}' not found in database");
    }
}


