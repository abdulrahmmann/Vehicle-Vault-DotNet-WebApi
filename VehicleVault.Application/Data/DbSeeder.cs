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
        var adminUsers = new List<(string UserName, string Email, string PhoneNumber)>
        {
            ("Admin.AbdulrahmanMustafa", "salim.vehicles.admin@admin.com", "0788362166"),
            ("Admin.OmarAbdullah", "omar.vehicles.admin@admin.com", "0788659200"),
            ("Admin.LaylaAmmar", "layla.vehicles.admin@admin.com", "0799452133"),
            ("Admin.RayanFares", "zaid.vehicles.admin@admin.com", "0777521369"),
            ("Admin.AseelHani", "nour.vehicles.admin@admin.com", "0784593022"),
        };

        foreach (var user in adminUsers)
        {
            var adminUser = await userManager.FindByEmailAsync(user.Email);
            
            if (adminUser == null)
            {
                logger.LogInformation($"Creating admin user: {user.UserName}...");

                adminUser = new ApplicationUser
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    LockoutEnabled = true,
                };

                var result = await userManager.CreateAsync(adminUser, "Admin_Password_123_@@##$$");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, Roles.Admin);
                    logger.LogInformation($"Admin user '{user.UserName}' created and added to ADMIN role.");
                }
                else
                {
                    logger.LogError("Failed to create admin user {UserName}: {Errors}", user.UserName, string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }
            else
            {
                logger.LogInformation($"Admin user '{user.UserName}' already exists.");
            }
        }
    }
}
