using Fynex.Communication.Requests;
using Fynex.Communication.Responses;

namespace Fynex.Application.UseCases.Expenses.Register;

public interface IRegisterExpenseUseCase
{
    Task<ResponseRegisteredExpenseJson> Execute(RequestExpenseJson request);
}
