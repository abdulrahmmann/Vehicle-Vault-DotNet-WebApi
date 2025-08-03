using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DriveType = VehicleVault.Domain.Entities.DriveType;

namespace VehicleVault.Infrastructure.Configurations;

public class DriveTypeConfiguration : BaseEntityConfiguration<DriveType>
{
    public override void Configure(EntityTypeBuilder<DriveType> builder)
    {
        base.Configure(builder);
        
        // Table_Name, Index, PK_Name
        builder.ToTable("DriveTypes");
        
        builder.Property(dt => dt.Id).HasColumnName("PK_DriveType_Id");
        
        builder.HasIndex(dt => dt.Name);
        
        
        // Columns_Name_Length

        builder.Property(dt => dt.Name).HasColumnName("DriveTypeName").HasMaxLength(60);
    }
}