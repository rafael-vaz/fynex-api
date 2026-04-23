using CommonTestUtilities.Entities;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using Fynex.Application.UseCases.Expenses.GetById;
using Fynex.Domain.Entities;
using Fynex.Exception;
using Fynex.Exception.ExceptionsBase;
using Shouldly;

namespace UseCases.Test.Expenses.GetById;

public class GetExpenseByUseCaseTest
{
    public async Task Success()
    {
        var loggedUser = UserBuilder.Build();
        var expense = ExpenseBuilder.Build(user:loggedUser);
        var useCase = CreateUseCase(loggedUser, expense);
        var result = await useCase.Execute(expense.Id);

        result.ShouldNotBeNull();
        result.Id.ShouldBe(expense.Id);
        result.Title.ShouldBe(expense.Title);
        result.Description.ShouldBe(expense.Description);
        result.Amount.ShouldBe(expense.Amount);
        result.Date.ShouldBe(expense.Date);
        result.PaymentType.ShouldBe((Fynex.Communication.Enums.PaymentType)expense.PaymentType);
    }

    public async Task Error_Expense_Not_Found()
    {
        var loggedUser = UserBuilder.Build();
        var useCase = CreateUseCase(loggedUser);
        var act = async () => await useCase.Execute(id: 1000);
        var result = await act.ShouldThrowAsync<NotFoundException>();

        result.GetErrors().Count.ShouldBe(1);
        result.GetErrors().ShouldContain(ResourceErrorMessages.EXPENSE_NOT_FOUND);
    }

    private GetExpenseByIdUseCase CreateUseCase(User user, Expense? expense = null)
    {
        var respository = new ExpensesReadOnlyRepositoryBuilder().GetById(user, expense).Build();
        var mapper = MapperBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(user);

        return new GetExpenseByIdUseCase(respository, mapper, loggedUser);
    }
}
