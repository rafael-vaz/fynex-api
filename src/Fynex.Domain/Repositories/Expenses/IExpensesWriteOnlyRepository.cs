using Fynex.Domain.Entities;

namespace Fynex.Domain.Repositories.Expenses;

public interface IExpensesWriteOnlyRepository
{
    Task Add(Expense expense);
    /// <summary>
    /// This function returns True if the expense was deleted successfully, otherwise it returns false. This is because the delete operation may fail if the expense with the given id does not exist in the database.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task Delete(long id);
}
