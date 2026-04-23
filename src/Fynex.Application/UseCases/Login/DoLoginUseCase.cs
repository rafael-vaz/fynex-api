using Fynex.Communication.Requests;
using Fynex.Communication.Responses;
using Fynex.Domain.Repositories.User;
using Fynex.Domain.Security.Cryptography;
using Fynex.Domain.Security.Tokens;
using Fynex.Exception.ExceptionsBase;

namespace Fynex.Application.UseCases.Login;

public class DoLoginUseCase : IDoLoginUseCase
{
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IPasswordEncripter _passwordEncripter;
    private readonly IAccessTokenGenerator _accessTokenGenerator;

    public DoLoginUseCase(
     IUserReadOnlyRepository repository,
     IPasswordEncripter passwordEncripter,
     IAccessTokenGenerator accessTokenGenerator
     )
    {
        _passwordEncripter = passwordEncripter;
        _userReadOnlyRepository = repository;
        _accessTokenGenerator = accessTokenGenerator;
    }

    public async Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request)
    {
        var user = await _userReadOnlyRepository.GetUserByEmail(request.Email);

        if (user is null)
        {
            throw new InvalidLoginException();
        }

        var passwordIsValid = _passwordEncripter.Verify(request.Password, user.Password);

        if (passwordIsValid == false)
        {
            throw new InvalidLoginException();
        }

        return new ResponseRegisteredUserJson
        {
            Name = user.Name,
            Token = _accessTokenGenerator.Generate(user)
        };
    }

}
