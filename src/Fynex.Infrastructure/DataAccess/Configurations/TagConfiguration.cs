using Fynex.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fynex.Infrastructure.DataAccess.Configurations;

public class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.HasOne(x => x.Expense)
            .WithMany(x => x.Tags)
            .HasForeignKey(x => x.ExpenseId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasData(
            new Tag { Id = 1, Value = Domain.Enums.Tag.Essential, ExpenseId = 1 },
            new Tag { Id = 2, Value = Domain.Enums.Tag.Fixed, ExpenseId = 1 },
            new Tag { Id = 3, Value = Domain.Enums.Tag.Essential, ExpenseId = 2 },
            new Tag { Id = 4, Value = Domain.Enums.Tag.Fixed, ExpenseId = 2 },
            new Tag { Id = 5, Value = Domain.Enums.Tag.Education, ExpenseId = 3 },
            new Tag { Id = 6, Value = Domain.Enums.Tag.Investment, ExpenseId = 3 },
            new Tag { Id = 7, Value = Domain.Enums.Tag.Education, ExpenseId = 4 },
            new Tag { Id = 8, Value = Domain.Enums.Tag.Personal, ExpenseId = 4 },
            new Tag { Id = 9, Value = Domain.Enums.Tag.Essential, ExpenseId = 5 },
            new Tag { Id = 10, Value = Domain.Enums.Tag.Variable, ExpenseId = 5 }
        );
    }
}
