namespace OOPClassLibrary.Games.Exceptions;

public class DlcCannotHaveOtherDlcException : Exception
{
    public DlcCannotHaveOtherDlcException(string? message) : base(message)
    {
    }
}
