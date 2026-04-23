using CommonTestUtilities.Entities;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using Fynex.Application.UseCases.Expenses.Register;
using Fynex.Domain.Entities;
using Fynex.Exception;
using Fynex.Exception.ExceptionsBase;
using Shouldly;

namespace UseCases.Test.Expenses.Register;

public class RegisterExpenseUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var loggedUser = UserBuilder.Build();
        var request = RequestExpenseJsonBuilder.Build();
        var useCase = CreateUseCase(loggedUser);
        var result = await useCase.Execute(request);

        result.ShouldNotBeNull();
        result.Title.ShouldBe(request.Title);
    }

    [Fact]
    public async Task Error_Title_Empty()
    {
      var loggedUser = UserBuilder.Build();
        var request = RequestExpenseJsonBuilder.Build();
        request.Title = string.Empty;

        var useCase = CreateUseCase(loggedUser);
        var act = async () => await useCase.Execute(request);
        var result = await act.ShouldThrowAsync<ErrorOnValidationException>();

        result.GetErrors().Count.ShouldBe(1);
        result.GetErrors().ShouldContain(ResourceErrorMessages.TITLE_REQUIRED);
    }

    private RegisterExpenseUseCase CreateUseCase(User user)
    {        
        var writeRepository = ExpenseWriteOnlyRepositoryBuilder.Build();
        var mapper = MapperBuilder.Build();
        var unitOfWork = UnitOfWorkBuilder.Build();   
        var loggedUser = LoggedUserBuilder.Build(user);

        return new RegisterExpenseUseCase(writeRepository, unitOfWork, mapper, loggedUser);
    }
}
