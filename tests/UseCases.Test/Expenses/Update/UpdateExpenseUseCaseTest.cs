using CommonTestUtilities.Entities;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using Fynex.Application.UseCases.Expenses.Update;
using Fynex.Domain.Entities;
using Fynex.Exception;
using Fynex.Exception.ExceptionsBase;
using Shouldly;


namespace UseCases.Test.Expenses.Update;

public class UpdateExpenseUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var loggedUser = UserBuilder.Build();
        var request = RequestExpenseJsonBuilder.Build();
        var expense = ExpenseBuilder.Build(loggedUser);
        var useCase = CreateUseCase(loggedUser, expense);
        var act = async () => await useCase.Execute(expense.Id, request);
        await act.ShouldNotThrowAsync();

        expense.Title.ShouldBe(request.Title);
        expense.Description.ShouldBe(request.Description);
        expense.Date.ShouldBe(request.Date);
        expense.Amount.ShouldBe(request.Amount);
        expense.PaymentType.ShouldBe((Fynex.Domain.Enums.PaymentType)request.PaymentType);
    }

    [Fact]
    public async Task Error_Title_Empty()
    {
        var loggedUser = UserBuilder.Build();
        var expense = ExpenseBuilder.Build(loggedUser);
        var request = RequestExpenseJsonBuilder.Build();
        request.Title = string.Empty;

        var useCase = CreateUseCase(loggedUser);
        var act = async () => await useCase.Execute(expense.Id, request);
        var result = await act.ShouldThrowAsync<ErrorOnValidationException>();
        result.GetErrors().Count.ShouldBe(1);
        result.GetErrors().ShouldContain(ResourceErrorMessages.TITLE_REQUIRED);
    }

    [Fact]
    public async Task Error_Expense_Not_Found() {
        var loggedUser = UserBuilder.Build();
        var request = RequestExpenseJsonBuilder.Build();
        var useCase = CreateUseCase(loggedUser);
        var act = async () => await useCase.Execute(id:1000, request);
        var result = await act.ShouldThrowAsync<NotFoundException>();
        result.GetErrors().Count.ShouldBe(1);
        result.GetErrors().ShouldContain(ResourceErrorMessages.EXPENSE_NOT_FOUND);
    }

    private UpdateExpenseUseCase CreateUseCase(User user, Expense? expense = null)
    {
        var respository = new ExpensesUpdateOnlyRepositoryBuilder().GetById(user, expense).Build();
        var mapper = MapperBuilder.Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(user);

        return new UpdateExpenseUseCase(mapper, unitOfWork, respository, loggedUser);
    }
}
