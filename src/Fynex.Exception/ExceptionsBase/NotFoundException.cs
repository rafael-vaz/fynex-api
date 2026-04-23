using System.Net;

namespace Fynex.Exception.ExceptionsBase;

public class NotFoundException : FynexException
{
    public NotFoundException(string message) : base(message)
    {
    }

    public override int StatusCode => (int)HttpStatusCode.NotFound;

    public override List<string> GetErrors()
    {
        return [Message];
    }
}
