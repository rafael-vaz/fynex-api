using Fynex.Communication.Requests;

namespace Fynex.Application.UseCases.Users.Update;

public interface IUpdateUserUseCase
{
    public Task Execute(RequestUpdateUserJson request);
}
