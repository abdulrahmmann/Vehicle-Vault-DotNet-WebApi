using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehicleVault.Domain.Entities;

namespace VehicleVault.Infrastructure.Configurations;

public class SubModelConfiguration : BaseEntityConfiguration<SubModel>
{
    public override void Configure(EntityTypeBuilder<SubModel> builder)
    {
        base.Configure(builder);
        
        // Table_Name, Index, PK_Name
        builder.ToTable("SubModels");
        
        builder.Property(m => m.Id).HasColumnName("PK_SubModel_Id");
        
        builder.HasIndex(m => m.Name);
        
        
        // Columns_Name_Length

        builder.Property(m => m.Name).HasColumnName("SubModelName").HasMaxLength(100);
        
        
        // Relations
        
        // Model & SubModels --> One_To_Many
        builder
            .HasOne(sm => sm.Model)
            .WithMany(mm => mm.SubModelsCollection)
            .HasForeignKey(sm => sm.ModelId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_SubModel_Model");
    }
}