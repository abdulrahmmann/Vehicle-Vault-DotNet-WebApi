using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehicleVault.Domain.Entities;

namespace VehicleVault.Infrastructure.Configurations;

public class ModelConfiguration : BaseEntityConfiguration<Model>
{
    public override void Configure(EntityTypeBuilder<Model> builder)
    {
        base.Configure(builder);
        
        // Table_Name, Index, PK_Name
        builder.ToTable("Models");
        
        builder.Property(m => m.Id).HasColumnName("PK_Model_Id");
        
        builder.HasIndex(m => m.Name);
        
        
        // Columns_Name_Length

        builder.Property(m => m.Name).HasColumnName("ModelName").HasMaxLength(100);
        
        
        // Relations
        
        // Model & Make --> One_To_Many
        builder
            .HasOne(mm => mm.Make)
            .WithMany(m => m.ModelsCollection)
            .HasForeignKey(mm => mm.MakeId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Model_Make");
    }
}