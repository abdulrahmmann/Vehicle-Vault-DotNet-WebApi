using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehicleVault.Domain.Common;

namespace VehicleVault.Infrastructure.Configurations;

public abstract class BaseEntityConfiguration<TEntity>: IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(be => be.Id);

        builder.Property(be => be.Id).UseIdentityColumn();
        
        builder.Property(e => e.CreatedOn)
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(e => e.ModifiedOn)
            .IsRequired(false);
    }
}