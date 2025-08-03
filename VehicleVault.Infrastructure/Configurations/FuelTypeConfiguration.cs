using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehicleVault.Domain.Entities;

namespace VehicleVault.Infrastructure.Configurations;

public class FuelTypeConfiguration : BaseEntityConfiguration<FuelType>
{
    public override void Configure(EntityTypeBuilder<FuelType> builder)
    {
        base.Configure(builder);
        
        // Table_Name, Index, PK_Name
        builder.ToTable("FuelTypes");
        
        builder.Property(ft => ft.Id).HasColumnName("PK_FuelType_Id");
        
        builder.HasIndex(ft => ft.Name);
        
        
        // Columns_Name_Length

        builder.Property(ft => ft.Name).HasColumnName("FuelTypeName").HasMaxLength(60);
    }
}