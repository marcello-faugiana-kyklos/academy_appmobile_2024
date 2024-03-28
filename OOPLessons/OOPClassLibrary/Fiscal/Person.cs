using System.Reflection;

namespace OOPClassLibrary.Fiscal;

public class Person
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly DateOnly _dateOfBirth;
    private readonly string _placeOfBirth;
    private string _fiscalCode;
    private readonly Gender _gender;
    private readonly MaritalStatus _maritalStatus;

    public string FirstName =>
        _firstName;

    public string LastName =>
        _lastName;

    public string PlaceOfBirth =>
        _placeOfBirth;

    public string FiscalCode =>
        _fiscalCode;

    public DateOnly DateOfBirth =>
        _dateOfBirth;

    public Gender Gender =>
        _gender;

    public MaritalStatus MaritalStatus =>
        _maritalStatus;
}
