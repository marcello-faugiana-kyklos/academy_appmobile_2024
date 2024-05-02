using GamesDal.Support;
using System.Text;

namespace OOPClassLibrary.Fiscal.IFaceMethod;

public class FiscalCodeBuilderIFace
{
    private static readonly Dictionary<char, int> evenControlCodes =
        new Dictionary<char, int>
        {
            { '0', 0 },
            { '1', 1 },
            { '2', 2 },
            { '3', 3 },
            { '4', 4 },
            { '5', 5 },
            { '6', 6 },
            { '7', 7 },
            { '8', 8 },
            { '9', 9 },
            { 'A', 0 },
            { 'B', 1 },
            { 'C', 2 },
            { 'D', 3 },
            { 'E', 4 },
            { 'F', 5 },
            { 'G', 6 },
            { 'H', 7 },
            { 'I', 8 },
            { 'J', 9 },
            { 'K', 10 },
            { 'L', 11 },
            { 'M', 12 },
            { 'N', 13 },
            { 'O', 14 },
            { 'P', 15 },
            { 'Q', 16 },
            { 'R', 17 },
            { 'S', 18 },
            { 'T', 19 },
            { 'U', 20 },
            { 'V', 21 },
            { 'W', 22 },
            { 'X', 23 },
            { 'Y', 24 },
            { 'Z', 25 }
        };

    private static readonly Dictionary<char, int> oddControlCodes =
        new Dictionary<char, int>
        {
                { '0', 1 },
                { '1', 0 },
                { '2', 5 },
                { '3', 7 },
                { '4', 9 },
                { '5', 13 },
                { '6', 15 },
                { '7', 17 },
                { '8', 19 },
                { '9', 21 },
                { 'A', 1 },
                { 'B', 0 },
                { 'C', 5 },
                { 'D', 7 },
                { 'E', 9 },
                { 'F', 13 },
                { 'G', 15 },
                { 'H', 17 },
                { 'I', 19 },
                { 'J', 21 },
                { 'K', 2 },
                { 'L', 4 },
                { 'M', 18 },
                { 'N', 20 },
                { 'O', 11 },
                { 'P', 3 },
                { 'Q', 6 },
                { 'R', 8 },
                { 'S', 12 },
                { 'T', 14 },
                { 'U', 16 },
                { 'V', 10 },
                { 'W', 22 },
                { 'X', 25 },
                { 'Y', 24 },
                { 'Z', 23 }
        };

    private static char[] monthLetters = 
        ['A', 'B', 'C', 'D', 'E', 'H', 'L', 'M', 'P', 'R', 'S', 'T'];

    private IPlaceOfBirthCodeRetriever _placeOfBirthCodeRetriever;

    public FiscalCodeBuilderIFace(IPlaceOfBirthCodeRetriever placeOfBirthCodeRetriever)
    {
        _placeOfBirthCodeRetriever = placeOfBirthCodeRetriever;
    }


    //public IPlaceOfBirthCodeRetriever PlaceOfBirthCodeRetriever { get; set; }

    public string Build
    (
        Person person
    )
    {
        person.AssertArgumentNotNull(nameof(person));

        StringBuilder fiscalCodeBuidler = new StringBuilder();

        fiscalCodeBuidler.Append(GetLastNamePart(person.LastName));
        fiscalCodeBuidler.Append(GetFirstNamePart(person.FirstName));
        fiscalCodeBuidler.Append(GetBirthDateAndGenderPart(person.DateOfBirth, person.Gender));
        fiscalCodeBuidler.Append(GetPlaceOfBirthCode(person.PlaceOfBirth));
        fiscalCodeBuidler.Append(GetControlCode(fiscalCodeBuidler.ToString()));

        return fiscalCodeBuidler.ToString();
    }

    private string GetLastNamePart(string lastName)
    {
        var (vowels, consonants) = SplitStringIntoVowelsAndConsonants(lastName);

        return
            (consonants + vowels)
            .PadRight(3, 'X')
            .Substring(0, 3);

    }

    private string GetFirstNamePart(string firstName)
    {
        var (vowels, consonants) = SplitStringIntoVowelsAndConsonants(firstName);

        if (consonants.Length > 3)
        {
            return $"{consonants[0]}{consonants[2]}{consonants[3]}";
        }

        return
            (consonants + vowels)
            .PadRight(3, 'X')
            .Substring(0, 3);
    }

    private static (string Vowels, string Consonants) SplitStringIntoVowelsAndConsonants(string s)
    {
        const string vowels = "AEIOU";

        char FixVowels(char c) =>
            c switch
            {
                'À' => 'A',
                'È' => 'E',
                'É' => 'E',
                'Ì' => 'I',
                'Ò' => 'O',
                'Ù' => 'U',
                _ => c
            };


        string vow = string.Empty;
        string cons = string.Empty;

        foreach (var c in s)
        {
            char cc = char.ToUpper(c);

            if (cc == '\'' || char.IsWhiteSpace(cc))
            {
                continue;
            }

            cc = FixVowels(cc);

            if (vowels.IndexOf(cc) > -1)
            {
                vow += cc;
            }
            else
            {
                cons += cc;
            }
        }

        return (vow, cons);
    }

    private string GetBirthDateAndGenderPart(DateOnly dateOfBirth, Gender gender)
    {
        string yearPart = (dateOfBirth.Year % 100).ToString("00");
        string monthPart = monthLetters[dateOfBirth.Month - 1].ToString();

        string dayPart = (dateOfBirth.Day + (gender == Gender.Female ? 40 : 0)).ToString("00");


        return yearPart + monthPart + dayPart;
    }

    private string GetPlaceOfBirthCode
    (
        string placeOfBirth
    ) =>
        _placeOfBirthCodeRetriever
        .GetPlaceOfBirthCode(placeOfBirth);

    
    private string GetControlCode(string partialFiscalCode)
    {
        int total = 0;
        for (int i = 0; i < partialFiscalCode.Length; i++)
        {
            total +=
                ((i + 1) % 2) switch
                {
                    0 => evenControlCodes[partialFiscalCode[i]],
                    1 => oddControlCodes[partialFiscalCode[i]],
                    _ => throw new Exception("???")
                };
        }

        return ((char)('A' + (total % 26))).ToString();
    }
}
