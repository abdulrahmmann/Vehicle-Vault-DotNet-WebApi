using System.Net;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.UserFeature.Commands.Requests;
using VehicleVault.Domain.IdentityEntities;
using VehicleVault.Application.Constants;

namespace VehicleVault.Application.Features.UserFeature.Commands.Handler;
public class CreateUsersListByAdminHandler : IRequestHandler<CreateUsersListByAdminRequest, UserResponse<Unit>>
{
    #region Instance Fields
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<CreateUsersListByAdminHandler> _logger;
    #endregion

    #region Constructor
    public CreateUsersListByAdminHandler(UserManager<ApplicationUser> userManager, ILogger<CreateUsersListByAdminHandler> logger)
    {
        _userManager = userManager;
        _logger = logger;
    }
    #endregion

    public async Task<UserResponse<Unit>> Handle(CreateUsersListByAdminRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var usersRequests = request.UsersDto?.ToList();
            if (usersRequests == null || usersRequests.Count == 0)
            {
                return UserResponse<Unit>.Failure("User list is empty.", HttpStatusCode.BadRequest);
            }

            var validUsersList = new List<ApplicationUser>();
            var invalidUsersList = new List<string>();

            foreach (var dto in usersRequests)
            {
                if (dto == null || string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.UserName) || string.IsNullOrWhiteSpace(dto.Password))
                {
                    _logger.LogWarning("Invalid or incomplete user DTO. Skipping.");
                    invalidUsersList.Add(dto?.Email ?? "UNKNOWN");
                    continue;
                }

                var mappedUser = new ApplicationUser
                {
                    UserName = dto.UserName,
                    Email = dto.Email,
                    PhoneNumber = dto.PhoneNumber,
                    
                    EmailConfirmed = true,
                    
                    
                    LockoutEnabled = true,
                };

                var createResult = await _userManager.CreateAsync(mappedUser, dto.Password);
                if (!createResult.Succeeded)
                {
                    _logger.LogError("Failed to create user {Email}: {Errors}", dto.Email, string.Join(", ", createResult.Errors.Select(e => e.Description)));
                    invalidUsersList.Add(dto.Email);
                    continue;
                }

                var role = string.IsNullOrWhiteSpace(dto.Role) ? Roles.User : dto.Role;
                var roleResult = await _userManager.AddToRoleAsync(mappedUser, role);

                if (!roleResult.Succeeded)
                {
                    _logger.LogError("Failed to assign role {Role} to user {Email}: {Errors}", role, dto.Email, string.Join(", ", roleResult.Errors.Select(e => e.Description)));
                    invalidUsersList.Add(dto.Email);
                    continue;
                }

                validUsersList.Add(mappedUser);
            }

            if (!validUsersList.Any())
            {
                return UserResponse<Unit>.Failure("Failed to add any user from the list.", HttpStatusCode.BadRequest);
            }

            int successCount = validUsersList.Count;
            int totalRequested = usersRequests.Count;

            string message = successCount == totalRequested
                ? "All users created successfully."
                : $"Successfully added {successCount} of {totalRequested} users. Failed: {string.Join(", ", invalidUsersList)}";

            return UserResponse<Unit>.Created(string.Join(", ", validUsersList.Select(u => u.Email)), message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create user list.");
            return UserResponse<Unit>.Failure("An unexpected error occurred while creating users.", HttpStatusCode.InternalServerError);
        }
    }
}
