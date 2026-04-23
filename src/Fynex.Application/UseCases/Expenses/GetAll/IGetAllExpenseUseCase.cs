using Fynex.Communication.Responses;

namespace Fynex.Application.UseCases.Expenses.GetAll;

public interface IGetAllExpenseUseCase
{
    Task<ResponseExpensesJson> Execute();
}
