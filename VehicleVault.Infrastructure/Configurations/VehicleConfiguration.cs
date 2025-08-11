using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehicleVault.Domain.Entities;

namespace VehicleVault.Infrastructure.Configurations;

public class VehicleConfiguration: BaseEntityConfiguration<Vehicle>
{
    public override void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        base.Configure(builder);
        
        // Table_Name, Index, PK_Name
        builder.ToTable("Vehicles");
        
        builder.Property(v => v.Id).HasColumnName("PK_Vehicle_Id");
        
        builder.HasIndex(v => v.MakeId);
        builder.HasIndex(v => v.SubModelId);
        builder.HasIndex(v => v.ModelId);
        builder.HasIndex(v => v.CategoryId);
        builder.HasIndex(v => v.DriveTypeId);
        builder.HasIndex(v => v.FuelTypeId);
        builder.HasIndex(v => v.BodyId);
        builder.HasIndex(v => v.TransmissionTypeId);
        builder.HasIndex(v => v.ColorId);
        
        
        // Columns_Name_Length
        builder.Property(v => v.Engine).HasMaxLength(100);
        
        builder.Property(v => v.EngineCc).HasColumnName("Engine_CC");
        
        builder.Property(v => v.EngineCylinders).HasColumnName("Engine_Cylinders");
        
        builder.Property(v => v.EngineLiterDisplay).HasColumnType("money").HasColumnName("Engine_Liter_Display");
        
        builder.Property(v => v.Name).HasMaxLength(150).HasColumnName("Vehicle_Display_Name");
        
        builder.Property(v => v.Description).HasMaxLength(2000).HasColumnName("Vehicle_Description");
        
        
        // Relations
        
        // Vehicle & Body --> One_To_Many
        builder
            .HasOne(v => v.Body)
            .WithMany(b => b.VehiclesCollection)
            .HasForeignKey(v => v.BodyId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Vehicle_Body");
        
        
        // Vehicle & DriveType --> One_To_Many
        builder
            .HasOne(v => v.DriveType)
            .WithMany(d => d.VehiclesCollection)
            .HasForeignKey(v => v.DriveTypeId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Vehicle_DriveType");
        
        
        // Vehicle & Make --> One_To_Many
        builder
            .HasOne(v => v.Make)
            .WithMany(d => d.VehiclesCollection)
            .HasForeignKey(v => v.MakeId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Vehicle_Make");
        
        
        // Vehicle & FuelType --> One_To_Many
        builder
            .HasOne(v => v.FuelType)
            .WithMany(d => d.VehiclesCollection)
            .HasForeignKey(v => v.FuelTypeId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Vehicle_FuelType");
        
        
        // Vehicle & Model --> One_To_Many
        builder
            .HasOne(v => v.Model)
            .WithMany(d => d.VehiclesCollection)
            .HasForeignKey(v => v.ModelId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Vehicle_Model");
        
        
        // Vehicle & SubModel --> One_To_Many
        builder
            .HasOne(v => v.SubModel)
            .WithMany(d => d.VehiclesCollection)
            .HasForeignKey(v => v.SubModelId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Vehicle_SubModel");
        
        
        // Vehicle & Color --> One_To_Many
        builder
            .HasOne(v => v.Color)
            .WithMany(d => d.VehiclesCollection)
            .HasForeignKey(v => v.ColorId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Vehicle_Color");
        
        
        // Vehicle & Category --> One_To_Many
        builder
            .HasOne(v => v.Category)
            .WithMany(d => d.VehiclesCollection)
            .HasForeignKey(v => v.CategoryId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Vehicle_Category");
        
        
        // Vehicle & TransmissionType --> One_To_Many
        builder
            .HasOne(v => v.TransmissionType)
            .WithMany(d => d.VehiclesCollection)
            .HasForeignKey(v => v.SubModelId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Vehicle_Transmission");
        
        
        // Vehicle & User --> One_To_Many
        builder
            .HasOne(v => v.User)
            .WithMany(u => u.VehiclesCollection)
            .HasForeignKey(v => v.UserId)
            .HasConstraintName("FK_Vehicle_User");
    }
}