using Fynex.Domain.Entities;
using Fynex.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fynex.Infrastructure.DataAccess.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasData(
            new User { Id = 1, Name = "Alice", Email = "alice@email.com", Password = "hashedpassword1", UserIdentifier = Guid.Parse("11111111-1111-1111-1111-111111111111"), Role = Roles.ADMIN },
            new User { Id = 2, Name = "Bob", Email = "bob@email.com", Password = "hashedpassword2", UserIdentifier = Guid.Parse("22222222-2222-2222-2222-222222222222"), Role = Roles.TEAM_MEMBER },
            new User { Id = 3, Name = "Carol", Email = "carol@email.com", Password = "hashedpassword3", UserIdentifier = Guid.Parse("33333333-3333-3333-3333-333333333333"), Role = Roles.TEAM_MEMBER },
            new User { Id = 4, Name = "David", Email = "david@email.com", Password = "hashedpassword4", UserIdentifier = Guid.Parse("44444444-4444-4444-4444-444444444444"), Role = Roles.TEAM_MEMBER },
            new User { Id = 5, Name = "Eve", Email = "eve@email.com", Password = "hashedpassword5", UserIdentifier = Guid.Parse("55555555-5555-5555-5555-555555555555"), Role = Roles.TEAM_MEMBER }
        );
    }
}
