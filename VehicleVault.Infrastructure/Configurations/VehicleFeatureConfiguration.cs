using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehicleVault.Domain.Entities;

namespace VehicleVault.Infrastructure.Configurations;

public class VehicleFeatureConfiguration: IEntityTypeConfiguration<VehicleFeature>
{
    public void Configure(EntityTypeBuilder<VehicleFeature> builder)
    {
        // Table_Name, Index, PK_Name
        builder.ToTable("VehicleFeatures");

        builder.HasKey(vf => new { vf.VehicleId, vf.FeatureId });

        builder.Property(vf => vf.VehicleId).HasColumnName("PK_Vehicle_Id");
        builder.Property(vf => vf.FeatureId).HasColumnName("PK_Feature_Id");
        
        builder.HasIndex(vf => vf.VehicleId);
        builder.HasIndex(vf => vf.FeatureId);
        
        // Relations
        builder
            .HasOne(vf => vf.Vehicle)
            .WithMany(v => v.VehicleFeatures)
            .HasForeignKey(vf => vf.VehicleId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Vehicle_VehicleFeatures");

        builder
            .HasOne(vf => vf.Feature)
            .WithMany(f => f.VehicleFeatures)
            .HasForeignKey(vf => vf.FeatureId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Feature_VehicleFeatures");
        
    }
}