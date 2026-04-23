using FluentValidation.Results;
using Fynex.Communication.Requests;
using Fynex.Domain.Entities;
using Fynex.Domain.Repositories;
using Fynex.Domain.Repositories.User;
using Fynex.Domain.Security.Cryptography;
using Fynex.Domain.Services.LoggedUser;
using Fynex.Exception;
using Fynex.Exception.ExceptionsBase;

namespace Fynex.Application.UseCases.Users.ChangePassword;

public class ChangePasswordUseCase : IChangePasswordUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IUserUpdateOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordEncripter _passwordEncripter;

    public ChangePasswordUseCase(ILoggedUser loggedUser, IUserUpdateOnlyRepository repository, IUnitOfWork unitOfWork, IPasswordEncripter passwordEncripter)
    {
        _loggedUser = loggedUser;
        _repository = repository;
        _unitOfWork = unitOfWork;
        _passwordEncripter = passwordEncripter;
    }


    public async Task Execute(RequestChangePasswordJson request)
    {
        var loggedUser = await _loggedUser.Get();
        Validate(request, loggedUser);
        var user = await _repository.GetById(loggedUser.Id);
       
        user.Password = _passwordEncripter.Encrypt(request.NewPassword);
        _repository.Update(user);
        await _unitOfWork.Commit();
    }

    private void Validate(RequestChangePasswordJson request, User loggedUser)
    {
        var validator = new ChangePasswordValidator();
        var result = validator.Validate(request);
        var passwordMatch = _passwordEncripter.Verify(request.Password, loggedUser.Password);

        if(passwordMatch == false)
        {
            result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMessages.PASSWORD_DIFFERENT_CURRENT_PASSWORD));

        }

        if (result.IsValid == false)
        {
            var errros = result.Errors.Select(e => e.ErrorMessage).ToList();

        }

        if (result.IsValid == false)
        {
            var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errors);
        }
    }
}
