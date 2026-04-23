using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using Fynex.Application.UseCases.Expenses.Reports.Pdf;
using Fynex.Domain.Entities;
using Shouldly;

namespace UseCases.Test.Reports.Pdf;

public class GenerateExpensesReportPdfUseCaseTest
{
    [Fact]
    public async Task Success()
    {
    var loggedUser = UserBuilder.Build();
    var expenses = ExpenseBuilder.Collection(loggedUser);        
    var useCase = CreateUseCase(loggedUser, expenses);
    var result = await useCase.Execute(DateOnly.FromDateTime(DateTime.Today));

    result.ShouldNotBeEmpty();
    }

    [Fact]
    public async Task Success_Empty()
    { 
    var loggedUser = UserBuilder.Build();
    var useCase = CreateUseCase(loggedUser, new List<Expense>());
    var result = await useCase.Execute(DateOnly.FromDateTime(DateTime.Today));

    result.ShouldBeEmpty();
    }

    private GenerateExpensesReportPdfUseCase CreateUseCase(User user, List<Expense> expenses)
    {
        var repository = new ExpensesReadOnlyRepositoryBuilder().FilterByMonth(user, expenses).Build();
        var loggedUser = LoggedUserBuilder.Build(user);

        return new GenerateExpensesReportPdfUseCase(repository, loggedUser);
    }
}
