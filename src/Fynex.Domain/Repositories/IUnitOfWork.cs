namespace Fynex.Domain.Repositories;

public interface IUnitOfWork
{
    Task Commit();
}
