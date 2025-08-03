using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehicleVault.Domain.Entities;

namespace VehicleVault.Infrastructure.Configurations;

public class CategoryConfiguration : BaseEntityConfiguration<Category>
{
    public override void Configure(EntityTypeBuilder<Category> builder)
    {
        base.Configure(builder);
        
        // Table_Name, Index, PK_Name
        builder.ToTable("Categories");
        
        builder.Property(m => m.Id).HasColumnName("PK_Category_Id");
        
        builder.HasIndex(m => m.Name);
        
        
        // Columns_Name_Length

        builder.Property(m => m.Name).HasColumnName("CategoryName").HasMaxLength(30);
    }
}