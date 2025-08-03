using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehicleVault.Domain.IdentityEntities;

namespace VehicleVault.Infrastructure.Configurations;

public class UserConfiguration: IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(u => u.UserName).HasMaxLength(6).HasMaxLength(60);
        builder.Property(u => u.Email).HasMaxLength(60);
    }
}