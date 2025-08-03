using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehicleVault.Domain.Entities;

namespace VehicleVault.Infrastructure.Configurations;

public class ColorConfiguration : BaseEntityConfiguration<Color>
{
    public override void Configure(EntityTypeBuilder<Color> builder)
    {
        base.Configure(builder);
        
        // Table_Name, Index, PK_Name
        builder.ToTable("Colors");
        
        builder.Property(m => m.Id).HasColumnName("PK_Color_Id");
        
        builder.HasIndex(m => m.Name);
        
        
        // Columns_Name_Length

        builder.Property(m => m.Name).HasColumnName("ColorCode").HasMaxLength(10);
    }
}