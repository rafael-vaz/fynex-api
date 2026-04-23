namespace Fynex.Domain.Security.Tokens;

public interface ITokenProvider
{
    string TokenOnRequest();
}
