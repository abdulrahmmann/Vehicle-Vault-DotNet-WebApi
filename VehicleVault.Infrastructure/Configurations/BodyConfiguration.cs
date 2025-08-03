using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehicleVault.Domain.Entities;

namespace VehicleVault.Infrastructure.Configurations;

public class BodyConfiguration: BaseEntityConfiguration<Body>
{
    public override void Configure(EntityTypeBuilder<Body> builder)
    {
        base.Configure(builder);
        
        // Table_Name, Index, PK_Name
        builder.ToTable("Bodies");
        
        builder.Property(b => b.Id).HasColumnName("PK_Body_Id");
        
        builder.HasIndex(b => b.Name);
        
        
        // Columns_Name_Length

        builder.Property(b => b.Name).HasColumnName("BodyName").HasMaxLength(60);
    }
}