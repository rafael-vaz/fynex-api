namespace Fynex.Exception.ExceptionsBase;

public abstract class FynexException : SystemException
{
    protected FynexException(string message) : base(message)
    {
    }
    public abstract int StatusCode { get; }
    public abstract List<string> GetErrors();
}
