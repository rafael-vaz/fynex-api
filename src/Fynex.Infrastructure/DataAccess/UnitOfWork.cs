using Fynex.Domain.Repositories;

namespace Fynex.Infrastructure.DataAccess;

internal class UnitOfWork : IUnitOfWork
{
    private readonly FynexDbContext _dbContext;

    public UnitOfWork(FynexDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Commit() => await _dbContext.SaveChangesAsync();
}
