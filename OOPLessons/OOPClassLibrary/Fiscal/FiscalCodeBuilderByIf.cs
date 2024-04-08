namespace OOPClassLibrary.Fiscal;

public class FiscalCodeBuilderByIf : AbstractFiscalCodeBuilder
{
    protected override string GetPlaceOfBirthCode(string placeOfBirth) =>
       GetPlaceOfBirthCodeByIf(placeOfBirth);

    private string GetPlaceOfBirthCodeByIf(string placeOfBirth)
    {
        if (string.Equals(placeOfBirth, "Mazara Del Vallo", StringComparison.CurrentCultureIgnoreCase))
        {
            return "F061";
        }

        if (string.Equals(placeOfBirth, "Prato", StringComparison.CurrentCultureIgnoreCase))
        {
            return "G999";
        }
        throw new Exception($"'{placeOfBirth}' not found in database");
    }
}
