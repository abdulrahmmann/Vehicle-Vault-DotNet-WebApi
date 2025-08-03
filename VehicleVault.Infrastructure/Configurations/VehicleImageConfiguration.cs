using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehicleVault.Domain.Entities;

namespace VehicleVault.Infrastructure.Configurations;

public class VehicleImageConfiguration: BaseEntityConfiguration<VehicleImage>
{
    public override void Configure(EntityTypeBuilder<VehicleImage> builder)
    {
        base.Configure(builder);
        
        // Table_Name, Index, PK_Name
        builder.ToTable("VehicleImages");
        
        builder.Property(vi => vi.Id).HasColumnName("PK_VehicleImage_Id");
        
        builder.HasIndex(vi => vi.IsPrimary);
        
        // Columns_Name_Length
        
        // Relations
        builder
            .HasOne(vi => vi.Vehicle)
            .WithMany(v => v.VehicleImages)
            .HasForeignKey(vi => vi.VehicleId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Vehicle_VehicleImages");
    }
}