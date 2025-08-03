using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehicleVault.Domain.Entities;

namespace VehicleVault.Infrastructure.Configurations;

public class TransmissionTypeConfiguration : BaseEntityConfiguration<TransmissionType>
{
    public override void Configure(EntityTypeBuilder<TransmissionType> builder)
    {
        base.Configure(builder);
        
        // Table_Name, Index, PK_Name
        builder.ToTable("TransmissionTypes");
        
        builder.Property(m => m.Id).HasColumnName("PK_TransmissionType_Id");
        
        builder.HasIndex(m => m.Name);
        
        
        // Columns_Name_Length

        builder.Property(m => m.Name).HasColumnName("TransmissionTypeName").HasMaxLength(30);
    }
}