using Fynex.Domain.Entities;
using Fynex.Infrastructure.DataAccess.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Fynex.Infrastructure.DataAccess;

public class FynexDbContext : DbContext
{
    public FynexDbContext(DbContextOptions<FynexDbContext> options) : base(options) { }
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Tag> Tags { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new ExpenseConfiguration());
        modelBuilder.ApplyConfiguration(new TagConfiguration());
    }
}
