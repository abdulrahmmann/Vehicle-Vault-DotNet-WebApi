using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace VehicleVault.Infrastructure.Context;

public class ApplicationDbContextFactory: IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseSqlServer("Data Source=DESKTOP-DM2MVOJ;Database=VehicleVault;Trusted_Connection=true;Encrypt=False;TrustServerCertificate=True;");

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}

