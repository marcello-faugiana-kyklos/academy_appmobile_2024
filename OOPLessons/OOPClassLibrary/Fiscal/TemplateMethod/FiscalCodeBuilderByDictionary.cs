using GamesDal.Support;

namespace OOPClassLibrary.Fiscal.TemplateMethod;

public class FiscalCodeBuilderByDictionary : AbstractFiscalCodeBuilder
{
    private static readonly Dictionary<string, string> belfioreCodes;

    static FiscalCodeBuilderByDictionary()
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

    protected override string GetPlaceOfBirthCode
    (
        string placeOfBirth
    ) =>
        GetPlaceOfBirthCodeByDictionary(placeOfBirth);

    private string GetPlaceOfBirthCodeByDictionary(string placeOfBirth)
    {
        if (belfioreCodes.TryGetValue(placeOfBirth, out string? code))
        {
            return code;
        }

        throw new Exception($"'{placeOfBirth}' not found in database");
    }
}
