using Fynex.Communication.Requests;
using Fynex.Communication.Responses;

namespace Fynex.Application.UseCases.Users.Register;

public interface IRegisterUserUseCase
{
    Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request);
}
