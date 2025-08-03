using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehicleVault.Domain.Entities;

namespace VehicleVault.Infrastructure.Configurations;

public class MakeConfiguration : BaseEntityConfiguration<Make>
{
    public override void Configure(EntityTypeBuilder<Make> builder)
    {
        base.Configure(builder);
        
        // Table_Name, Index, PK_Name
        builder.ToTable("Makes");
        
        builder.Property(m => m.Id).HasColumnName("PK_Make_Id");
        
        builder.HasIndex(m => m.Name);
        
        
        // Columns_Name_Length

        builder.Property(m => m.Name).HasColumnName("MakeName").HasMaxLength(100);
    }
}