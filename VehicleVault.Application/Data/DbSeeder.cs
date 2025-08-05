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

        // List of default admin users to create
        var adminUsers = new List<(string UserName, string Email)>
        {
            ("admin1", "admin1@example.com"),
            ("admin2", "admin2@example.com"),
            ("admin3", "admin3@example.com"),
        };

        foreach (var (userName, email) in adminUsers)
        {
            var adminUser = await userManager.FindByEmailAsync(email);
            if (adminUser == null)
            {
                logger.LogInformation($"Creating admin user: {userName}...");

                adminUser = new ApplicationUser
                {
                    UserName = userName,
                    Email = email,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, "Admin@123");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, Roles.Admin);
                    logger.LogInformation($"Admin user '{userName}' created and added to ADMIN role.");
                }
                else
                {
                    logger.LogError("Failed to create admin user {UserName}: {Errors}", userName, string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }
            else
            {
                logger.LogInformation($"Admin user '{userName}' already exists.");
            }
        }
    }
}
