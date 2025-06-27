using Lootsy.UserService.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lootsy.UserService.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.FullName)
            .HasMaxLength(250)
            .IsRequired();

        builder.Property(u => u.PhoneNumber)
            .HasMaxLength(20)
            .IsRequired();
    }
}
