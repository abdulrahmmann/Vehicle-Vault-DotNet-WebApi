using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VehicleVault.Domain.Entities;
using VehicleVault.Domain.IdentityEntities;
using VehicleVault.Domain.Views;
using DriveType = VehicleVault.Domain.Entities.DriveType;

namespace VehicleVault.Infrastructure.Context;

public class ApplicationDbContext: IdentityDbContext<ApplicationUser, ApplicationRole, int>
{
    public virtual DbSet<Vehicle> Vehicles { get; set; }

    public virtual DbSet<Body> Bodies { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Color> Colors { get; set; }

    public virtual DbSet<DriveType> DriveTypes { get; set; }
    
    public virtual DbSet<FuelType> FuelTypes { get; set; }
    
    public virtual DbSet<Make> Makes { get; set; }
    
    public virtual DbSet<Model> Models { get; set; }
    
    public virtual DbSet<SubModel> SubModels { get; set; }
    
    public virtual DbSet<TransmissionType> TransmissionTypes { get; set; }
    
    public virtual DbSet<VehicleImage> VehicleImages { get; set; }
    
    public virtual DbSet<Feature> Features { get; set; }
    
    public virtual DbSet<VehicleFeature> VehicleFeatures { get; set; }
    
    protected ApplicationDbContext() { }
    
    public ApplicationDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        builder.Entity<VehiclesSummaryView>(entity =>
        {
            entity.HasNoKey();
            
            entity.ToView("VehiclesSummaryView");
        });
        
        builder.Entity<ApplicationUser>().HasQueryFilter(u => !u.IsDeleted);
        builder.Entity<Category>().HasQueryFilter(u => !u.IsDeleted);
        builder.Entity<Body>().HasQueryFilter(u => !u.IsDeleted);
        builder.Entity<Color>().HasQueryFilter(u => !u.IsDeleted);
        builder.Entity<DriveType>().HasQueryFilter(u => !u.IsDeleted);
        builder.Entity<Feature>().HasQueryFilter(u => !u.IsDeleted);
        builder.Entity<FuelType>().HasQueryFilter(u => !u.IsDeleted);
        builder.Entity<Make>().HasQueryFilter(u => !u.IsDeleted);
        builder.Entity<Model>().HasQueryFilter(u => !u.IsDeleted);
        builder.Entity<SubModel>().HasQueryFilter(u => !u.IsDeleted);
        builder.Entity<TransmissionType>().HasQueryFilter(u => !u.IsDeleted);
        builder.Entity<Vehicle>().HasQueryFilter(u => !u.IsDeleted);
        builder.Entity<VehicleFeature>().HasQueryFilter(u => !u.IsDeleted);
        builder.Entity<VehicleImage>().HasQueryFilter(u => !u.IsDeleted);
    }
}