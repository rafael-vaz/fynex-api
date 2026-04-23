using Fynex.Domain.Entities;

namespace Fynex.Domain.Services.LoggedUser;

public interface ILoggedUser
{
    public Task<User> Get();
}
