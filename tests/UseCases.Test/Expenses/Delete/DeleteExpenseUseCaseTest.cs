using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using Fynex.Application.UseCases.Expenses.Delete;
using Fynex.Domain.Entities;
using Fynex.Exception;
using Fynex.Exception.ExceptionsBase;
using Shouldly;

namespace UseCases.Test.Expenses.Delete;

public class DeleteExpenseUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var loggedUser = UserBuilder.Build();
        var expense = ExpenseBuilder.Build(loggedUser);
        var useCase = CreateUseCase(loggedUser, expense);
        var act = async () => await useCase.Execute(expense.Id);
        await act.ShouldNotThrowAsync();
    }


    [Fact]
    public async Task Error_Expense_Not_Found()
    {
        var loggedUser = UserBuilder.Build();
        var useCase = CreateUseCase(loggedUser);
        var act = async () => await useCase.Execute(id: 100);
        var result = await act.ShouldThrowAsync<NotFoundException>();
        result.GetErrors().Count.ShouldBe(1);
        result.GetErrors().ShouldContain(ResourceErrorMessages.EXPENSE_NOT_FOUND);
    }

    private DeleteExpenseUseCase CreateUseCase(User user, Expense? expense = null)
    {
        var repositoryWriteOnly = ExpenseWriteOnlyRepositoryBuilder.Build();
        var repositoryReadOnly = new ExpensesReadOnlyRepositoryBuilder().GetById(user, expense).Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(user);

        return new DeleteExpenseUseCase(repositoryWriteOnly, unitOfWork, loggedUser, repositoryReadOnly);
    }
}
