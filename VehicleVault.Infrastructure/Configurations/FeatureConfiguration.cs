using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehicleVault.Domain.Entities;

namespace VehicleVault.Infrastructure.Configurations;

public class FeatureConfiguration: BaseEntityConfiguration<Feature>
{
    public override void Configure(EntityTypeBuilder<Feature> builder)
    {
        base.Configure(builder);
        
        // Table_Name, Index, PK_Name
        builder.ToTable("Features");
        
        builder.Property(b => b.Id).HasColumnName("PK_Feature_Id");
        
        builder.HasIndex(b => b.Name);
        
        
        // Columns_Name_Length

        builder.Property(b => b.Name).HasColumnName("FeatureName").HasMaxLength(60);
    }
}