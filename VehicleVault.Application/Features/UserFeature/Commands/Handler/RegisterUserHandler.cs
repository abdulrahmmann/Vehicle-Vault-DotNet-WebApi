using System.Net;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.UserFeature.Commands.Requests;
using VehicleVault.Application.Features.UserFeature.DTOs;
using VehicleVault.Application.Services.Tokens;
using VehicleVault.Application.Services.Tokens.GenerateToken;
using VehicleVault.Domain.IdentityEntities;

namespace VehicleVault.Application.Features.UserFeature.Commands.Handler;

public class RegisterUserHandler: IRequestHandler<RegisterUserRequest, AuthenticationResponse>
{
    #region Instance Fields
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly IValidator<RegisterUserDto> _validator;
    private readonly IGenerateTokenService _tokenService;
    private readonly ILogger<RegisterUserHandler> _logger;
    #endregion

    #region Constructor
    public RegisterUserHandler(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, 
        RoleManager<ApplicationRole> roleManager, IValidator<RegisterUserDto> validator, IGenerateTokenService tokenService, 
        ILogger<RegisterUserHandler> logger)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _validator = validator;
        _tokenService = tokenService;
        _logger = logger;
    }
    #endregion
    
    public async Task<AuthenticationResponse> Handle(RegisterUserRequest request, CancellationToken cancellationToken)
    {
         try 
         { 
             var userRequest = request.UserDto;

            // 1. Validate input DTO
            var validationResult = await _validator.ValidateAsync(userRequest, cancellationToken);
            
            if (!validationResult.IsValid)
            {
                var validationErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                _logger.LogWarning("Validation failed: {Errors}", string.Join(", ", validationErrors));
                return AuthenticationResponse.Failure("Validation failed.", validationErrors, HttpStatusCode.BadRequest);
            }

            // 2. Check if email exists
            if (await _userManager.FindByEmailAsync(userRequest.Email) is not null)
            {
                return AuthenticationResponse.Failure("Email already exists.", statusCode: HttpStatusCode.Conflict);
            }

            // 3. Check if username exists
            if (await _userManager.FindByNameAsync(userRequest.UserName) is not null)
            {
                return AuthenticationResponse.Failure("Username already exists.", statusCode: HttpStatusCode.Conflict);
            }

            // 4. Create user
            var newUser = new ApplicationUser
            {
                Email = userRequest.Email,
                UserName = userRequest.UserName,
                PhoneNumber = userRequest.PhoneNumber,
            };

            var identityResult = await _userManager.CreateAsync(newUser, userRequest.Password);
            if (!identityResult.Succeeded)
            {
                var createErrors = identityResult.Errors.Select(e => e.Description).ToList();
                _logger.LogError("User creation failed: {Errors}", string.Join(", ", createErrors));
                return AuthenticationResponse.Failure("User registration failed.", createErrors, HttpStatusCode.BadRequest);
            }

            // 5. Assign role
            var roleResult = await _userManager.AddToRoleAsync(newUser, "User");
            if (!roleResult.Succeeded)
            {
                var roleErrors = roleResult.Errors.Select(e => e.Description).ToList();
                _logger.LogWarning("Role assignment failed: {Errors}", string.Join(", ", roleErrors));
                // Continue anyway, optional
            }

            // 6. Generate token
            var tokenResponse = _tokenService.GenerateToken(newUser);

            newUser.RefreshToken = tokenResponse.RefreshToken;
            newUser.RefreshTokenExpiration = tokenResponse.RefreshTokenExpiration;
            
            await _userManager.UpdateAsync(newUser);
            
            return tokenResponse; 
         }
         catch (Exception ex) 
         { 
             _logger.LogError(ex, "Unexpected error while registering user: {Email}", request.UserDto.Email); 
             return AuthenticationResponse.Failure("Unexpected server error. Please try again later."); 
         } 
    }
}