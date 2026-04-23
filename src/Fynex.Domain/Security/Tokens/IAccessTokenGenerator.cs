using Fynex.Domain.Entities;

namespace Fynex.Domain.Security.Tokens;

public interface IAccessTokenGenerator
{
    string Generate(User user);
}
