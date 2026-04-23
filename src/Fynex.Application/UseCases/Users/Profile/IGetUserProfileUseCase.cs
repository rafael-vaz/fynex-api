using Fynex.Communication.Responses;

namespace Fynex.Application.UseCases.Users.Profile;

public interface IGetUserProfileUseCase
{
    Task<ResponseUserProfileJson> Execute();
}
