using Fynex.Domain.Entities;
using Fynex.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fynex.Infrastructure.DataAccess.Configurations;

public class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
{
    public void Configure(EntityTypeBuilder<Expense> builder)
    {
        builder.Property(x => x.Amount)
            .HasPrecision(18, 2);

        builder.HasData(
            new Expense
            {
                Id = 1,
                Title = "Office Supplies",
                Description = "Notebooks and pens",
                Date = new DateTime(2025, 1, 10, 0, 0, 0, DateTimeKind.Utc),
                Amount = 150.75m,
                PaymentType = PaymentType.CreditCard,
                UserId = 1
            },
            new Expense
            {
                Id = 2,
                Title = "Internet",
                Description = "Monthly plan",
                Date = new DateTime(2025, 1, 15, 0, 0, 0, DateTimeKind.Utc),
                Amount = 99.90m,
                PaymentType = PaymentType.ElectronicTransfer,
                UserId = 2
            },
            new Expense
            {
                Id = 3,
                Title = "Conference Ticket",
                Description = "Tech conference 2025",
                Date = new DateTime(2025, 2, 5, 0, 0, 0, DateTimeKind.Utc),
                Amount = 350.00m,
                PaymentType = PaymentType.CreditCard,
                UserId = 3
            },
            new Expense
            {
                Id = 4,
                Title = "Books",
                Description = "Programming books",
                Date = new DateTime(2025, 2, 10, 0, 0, 0, DateTimeKind.Utc),
                Amount = 120.00m,
                PaymentType = PaymentType.DebitCard,
                UserId = 4
            },
            new Expense
            {
                Id = 5,
                Title = "Coffee",
                Description = "Coffee for the team",
                Date = new DateTime(2025, 2, 15, 0, 0, 0, DateTimeKind.Utc),
                Amount = 45.50m,
                PaymentType = PaymentType.ElectronicTransfer,
                UserId = 5
            }
        );
    }
}
