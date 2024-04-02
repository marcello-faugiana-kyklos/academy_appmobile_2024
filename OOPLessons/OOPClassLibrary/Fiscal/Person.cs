using System.Linq;
using System.Reflection;

namespace OOPClassLibrary.Fiscal;

public class Person
{
    //private readonly string _firstName;
    //public string FirstName =>
    //    _firstName;

    // Auto property in solo get. Readonly



    //private readonly string _lastName;



    //private readonly DateOnly _dateOfBirth;
    //private readonly string _placeOfBirth;
    //private string _fiscalCode;
    //private readonly Gender _gender;
    //private readonly MaritalStatus _maritalStatus;

    private static readonly DateOnly MinValidDate = new DateOnly(1900, 1, 1);

    public string FirstName { get; }
    public string LastName { get; }
    public string PlaceOfBirth { get; }

    private string? _fiscalCode;
    public string? FiscalCode 
    {
        get => _fiscalCode;
        set
        {
            if (_fiscalCode is not null)
            {
                throw new Exception("Fiscal code already set");
            }
            _fiscalCode = SanitizeFiscalCode(value);
        }
    }
    
    public DateOnly DateOfBirth { get; }
    public Gender Gender { get; }
    public MaritalStatus MaritalStatus { get; }

    public Person
    (
        string firstName,
        string lastName,
        string placeOfBirth,
        DateOnly dateOfBirth,
        Gender gender,
        MaritalStatus maritalStatus
    ) : this
        (
            firstName,
            lastName,
            placeOfBirth,
            null,
            dateOfBirth,
            gender,
            maritalStatus
        )
    {

    }

    public Person
    (
        string firstName, 
        string lastName, 
        string placeOfBirth, 
        string? fiscalCode, 
        DateOnly dateOfBirth, 
        Gender gender, 
        MaritalStatus maritalStatus
    )
    {
        FirstName = SanitizeName(firstName, nameof(firstName));
        LastName = SanitizeName(lastName, nameof(lastName));
        PlaceOfBirth = SanitizeName(placeOfBirth, nameof(placeOfBirth));
        _fiscalCode = fiscalCode is null ? null : SanitizeFiscalCode(fiscalCode);
        DateOfBirth = SanitizeDateOfBirth(dateOfBirth);
        Gender = gender;
        MaritalStatus = maritalStatus;
    }

    private static string SanitizeName(string name, string paramName)
    {
        const int nameMimLen = 2;
        const int nameMaxLen = 255;

        if (name is null)
        {
            throw new ArgumentNullException(paramName);
        }

        string sanitizedName = name.Trim();

        if (sanitizedName.Length < nameMimLen)
        {
            throw new ArgumentException($"{paramName} must be at least {nameMimLen} chars");
        }

        if (sanitizedName.Length > nameMaxLen)
        {
            throw new ArgumentException($"{paramName} must be less than {nameMaxLen} chars");
        }

        string invalidChars = "0123456789,.;:!$£%()@§/\t\n\r\"";

        foreach (char invalidChar in invalidChars)
        {
            if (sanitizedName.Contains(invalidChar))
            {
                throw new ArgumentException($"{paramName} cannot contain char {invalidChar}");
            }
        }

        //for (int i = 0; i < invalidChars.Length; i++)
        //{
        //    char invalidChar = invalidChars[i];
        //    if (sanitizedName.Contains(invalidChar))
        //    {
        //        throw new ArgumentException($"{paramName} cannot contain char {invalidChar}");
        //    }
        //}



        return sanitizedName;
    }

    
    private DateOnly SanitizeDateOfBirth(DateOnly date) =>
        IsDateOfBirthValid(date) ? date : throw new ArgumentException($"Invalid date of birth: {DateOfBirth:yyyy-MM-dd}");

    private static bool IsDateOfBirthValid(DateOnly date) =>
        date <= DateOnly.FromDateTime(DateTime.Today)
        && (date >= MinValidDate);

    private static string SanitizeFiscalCode(string? fiscalCode)
    {
        if (fiscalCode is null)
        {
            throw new ArgumentNullException(nameof(fiscalCode));
        }
        if (fiscalCode.Length != 16)
        {
            throw new ArgumentException("Fiscal code must have length 16");
        }
        return fiscalCode;
    }

    public override string ToString()
    {
        return $"Hi, I'm {LastName}, {FirstName} {LastName}";
    }
}
