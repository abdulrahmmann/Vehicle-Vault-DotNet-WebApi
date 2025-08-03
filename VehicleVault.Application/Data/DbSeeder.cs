using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using VehicleVault.Application.Constants;
using VehicleVault.Domain.IdentityEntities;

namespace VehicleVault.Application.Data;

public class DbSeeder
{
    public static async Task SeedData(IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<DbSeeder>>();
        
        try
        {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>(); // <-- Use ApplicationRole here!

            // Ensure the Admin role exists
            if (!await roleManager.RoleExistsAsync(Roles.Admin))
            {
                logger.LogInformation("Creating Admin role...");
                var roleResult = await roleManager.CreateAsync(new ApplicationRole { Name = Roles.Admin });
                if (!roleResult.Succeeded)
                {
                    logger.LogError("Failed to create Admin role: {Errors}", string.Join(", ", roleResult.Errors.Select(e => e.Description)));
                    return;
                }
                logger.LogInformation("Admin role created.");
            }

            var admins = new List<(string Name, string Email, string Password)>
            {
                ("Admin Abdulrahman_Mustafa", "admin1abdulrahman@gmail.com", "Admin@123"),
                ("Admin Mera_Rahal", "admin2mera@gmail.com", "Admin@456"),
                ("Admin Ruaa_Samer", "admin3ruaa@gmail.com", "Admin@789"),
                ("Admin Aseel_Ali", "admin4assel@gmail.com", "Admin@101"),
                ("Admin Ayah_Abdullah", "admin5ayah@gmail.com", "Admin@202")
            };

            foreach (var (name, email, password) in admins)
            {
                if (await userManager.FindByEmailAsync(email) != null)
                {
                    logger.LogInformation("Admin with email {Email} already exists.", email);
                    continue;
                }

                var user = new ApplicationUser
                {
                    PersonName = name,
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                var createResult = await userManager.CreateAsync(user, password);
                if (!createResult.Succeeded)
                {
                    logger.LogError("Failed to create user {Email}. Errors: {Errors}", email, string.Join(", ", createResult.Errors.Select(e => e.Description)));
                    continue;
                }

                var roleResult = await userManager.AddToRoleAsync(user, Roles.Admin);
                if (!roleResult.Succeeded)
                {
                    logger.LogError("Failed to assign Admin role to {Email}. Errors: {Errors}", email, string.Join(", ", roleResult.Errors.Select(e => e.Description)));
                }

                logger.LogInformation("Admin user {Email} created and assigned to Admin role.", email);
            }
        }
        catch (Exception ex)
        {
            logger.LogCritical(ex, "An error occurred while seeding admin users.");
        }
    }
}

