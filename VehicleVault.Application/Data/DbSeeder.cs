using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using VehicleVault.Application.Constants;
using VehicleVault.Domain.IdentityEntities;

public class DbSeeder
{
    public static async Task SeedRolesAndAdminAsync(IServiceProvider serviceProvider)
    {
        var logger = serviceProvider.GetRequiredService<ILogger<DbSeeder>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        // Ensure ADMIN role exists
        if (!await roleManager.RoleExistsAsync(Roles.Admin))
        {
            logger.LogInformation("Creating ADMIN role...");
            var adminRoleResult = await roleManager.CreateAsync(new ApplicationRole { Name = Roles.Admin });
            if (!adminRoleResult.Succeeded)
            {
                logger.LogError("Failed to create ADMIN role: {Errors}", string.Join(", ", adminRoleResult.Errors.Select(e => e.Description)));
                return;
            }
            logger.LogInformation("ADMIN role created.");
        }

        // Ensure USER role exists
        if (!await roleManager.RoleExistsAsync(Roles.User))
        {
            logger.LogInformation("Creating USER role...");
            var userRoleResult = await roleManager.CreateAsync(new ApplicationRole { Name = Roles.User });
            if (!userRoleResult.Succeeded)
            {
                logger.LogError("Failed to create USER role: {Errors}", string.Join(", ", userRoleResult.Errors.Select(e => e.Description)));
                return;
            }
            logger.LogInformation("USER role created.");
        }

        // Create default admin user if not exists
        var adminEmail = "admin@example.com";
        var adminUser = await userManager.FindByEmailAsync(adminEmail);
        if (adminUser == null)
        {
            logger.LogInformation("Creating default admin user...");
            adminUser = new ApplicationUser
            {
                UserName = "admin",
                Email = adminEmail,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(adminUser, "Admin@123");

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, Roles.Admin);
                logger.LogInformation("Default admin user created and added to ADMIN role.");
            }
            else
            {
                logger.LogError("Failed to create default admin user: {Errors}", string.Join(", ", result.Errors.Select(e => e.Description)));
            }
        }
        else
        {
            logger.LogInformation("Default admin user already exists.");
        }
    }
}
