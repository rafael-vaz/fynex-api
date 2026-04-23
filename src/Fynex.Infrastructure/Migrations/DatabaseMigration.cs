using Fynex.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Fynex.Infrastructure.Migrations;

public static class DatabaseMigration
{
    public async static Task MigrateDatabase(IServiceProvider serviceProvider)
    {
        var dbContext = serviceProvider.GetRequiredService<FynexDbContext>();
        await dbContext.Database.MigrateAsync();
    }
}
