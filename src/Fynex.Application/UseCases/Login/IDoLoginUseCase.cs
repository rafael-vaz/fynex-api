using Fynex.Communication.Requests;
using Fynex.Communication.Responses;

namespace Fynex.Application.UseCases.Login;

public interface IDoLoginUseCase
{
    Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request);
}
