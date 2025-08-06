using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using VehicleVault.Application.Features.UserFeature.Commands.Requests;
using VehicleVault.Application.Features.UserFeature.DTOs;
using VehicleVault.Application.Services.Tokens.GenerateToken;
using VehicleVault.Domain.IdentityEntities;

public class RegisterUsersListByAdminHandler : IRequestHandler<RegisterUsersListByAdminRequest, string>
{
    #region Instance Fields
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IValidator<RegisterUserDtoByAdmin> _validator;
    private readonly IGenerateTokenService _tokenService;
    private readonly ILogger<RegisterUsersListByAdminHandler> _logger;
    #endregion

    #region Constructor
    public RegisterUsersListByAdminHandler(UserManager<ApplicationUser> userManager, IValidator<RegisterUserDtoByAdmin> validator,
        IGenerateTokenService tokenService, ILogger<RegisterUsersListByAdminHandler> logger) 
    {
        _userManager = userManager;
        _validator = validator;
        _tokenService = tokenService;
        _logger = logger;
    }
    #endregion

    public async Task<string> Handle(RegisterUsersListByAdminRequest byAdminRequest, CancellationToken cancellationToken)
    {
        try
        {
            var validUsersToAdd = new List<ApplicationUser>();
            var invalidUsersToRemove = new List<string>();

            foreach (var dto in byAdminRequest.UsersDto)
            {
                var validationResult = await _validator.ValidateAsync(dto, cancellationToken);
                if (!validationResult.IsValid)
                {
                    _logger.LogWarning("Validation failed for {Email}", dto.Email);
                    invalidUsersToRemove.Add(dto.Email);
                    continue;
                }

                if (await _userManager.FindByEmailAsync(dto.Email) is not null)
                {
                    _logger.LogWarning("Email already exists: {Email}", dto.Email);
                    invalidUsersToRemove.Add(dto.Email);
                    continue;
                }

                if (await _userManager.FindByNameAsync(dto.UserName) is not null)
                {
                    _logger.LogWarning("Username already exists: {Username}", dto.UserName);
                    invalidUsersToRemove.Add(dto.Email);
                    continue;
                }

                var newUser = new ApplicationUser
                {
                    Email = dto.Email,
                    UserName = dto.UserName,
                    PhoneNumber = dto.PhoneNumber,
                    
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    LockoutEnabled = true,
                };

                var identityResult = await _userManager.CreateAsync(newUser, dto.Password);
                if (!identityResult.Succeeded)
                {
                    _logger.LogError("User creation failed for {Email}: {Errors}", dto.Email, string.Join(", ", identityResult.Errors.Select(e => e.Description)));
                    invalidUsersToRemove.Add(dto.Email);
                    continue;
                }

                var roleResult = await _userManager.AddToRoleAsync(newUser, dto.Role ?? "User");
                if (!roleResult.Succeeded)
                {
                    _logger.LogWarning("Role assignment failed for {Email}: {Errors}", newUser.Email, string.Join(", ", roleResult.Errors.Select(e => e.Description)));
                }

                var tokenResponse = _tokenService.GenerateToken(newUser);
                newUser.RefreshToken = tokenResponse.RefreshToken;
                newUser.RefreshTokenExpiration = tokenResponse.RefreshTokenExpiration;

                await _userManager.UpdateAsync(newUser);

                validUsersToAdd.Add(newUser);
            }

            // Return string result
            var successCount = validUsersToAdd.Count;
            var failedCount = invalidUsersToRemove.Count;

            if (successCount == 0)
                return $"No users were registered. Failed emails: {string.Join(", ", invalidUsersToRemove)}";

            return $"Registered {successCount} users successfully. " +
                   (failedCount > 0 ? $"Failed to register {failedCount}: {string.Join(", ", invalidUsersToRemove)}" : "All users registered successfully.");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Unexpected error while registering list of users");
            return "Unexpected server error. Please try again later.";
        }
    }
}
