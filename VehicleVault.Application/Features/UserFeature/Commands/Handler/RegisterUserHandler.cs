using System.Net;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.UserFeature.Commands.Requests;
using VehicleVault.Application.Features.UserFeature.DTOs;
using VehicleVault.Application.Services.Tokens;
using VehicleVault.Domain.IdentityEntities;

namespace VehicleVault.Application.Features.UserFeature.Commands.Handler;

public class RegisterUserHandler: IRequestHandler<RegisterUserRequest, AuthenticationResponse>
{
    #region Instance Fields
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly IValidator<RegisterUserDto> _validator;
    private readonly ITokenService _tokenService;
    private readonly ILogger<RegisterUserHandler> _logger;
    #endregion

    #region Constructor
    public RegisterUserHandler(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager, IValidator<RegisterUserDto> validator, ITokenService tokenService, ILogger<RegisterUserHandler> logger)
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

            var validationResult = await _validator.ValidateAsync(request.UserDto, cancellationToken);

            if (!validationResult.IsValid)
            {
                var validationErrors = string.Join(",", validationResult.Errors.Select(e => e.ErrorMessage));
                _logger.LogError("Validation Error: {errors}", validationErrors);
                return AuthenticationResponse.Failure($"Validation Error: {validationErrors}");
            }
            
            // Check Existing Email.
            var isEmailExist = await _userManager.FindByEmailAsync(userRequest.Email);
            
            if (isEmailExist != null)
            {
                return AuthenticationResponse.Failure(message:"Email is Already Exist", statusCode:HttpStatusCode.Conflict);
            }
            
            // Check Existing Username.
            var isUsernameExist = await _userManager.FindByNameAsync(userRequest.UserName);
            
            if (isUsernameExist != null)
            {
                return AuthenticationResponse.Failure(message:"Email is Already Exist", statusCode:HttpStatusCode.Conflict);
            }
            
            // Create New User and Map UserRegisterDto To ApplicationUser.
            var newUser = new ApplicationUser
            {
                Email = userRequest.Email,
                UserName = userRequest.UserName,
                PhoneNumber = userRequest.PhoneNumber,
            };
            
            // Check Identity Result Of Creating New User.
            var identityResult = await _userManager.CreateAsync(newUser, userRequest.Password);
            
            if (!identityResult.Succeeded)
            {
                return AuthenticationResponse.Failure(
                    message: "User Registration Failed", 
                    errors: identityResult.Errors.Select(err => err.Description).ToList(), 
                    statusCode:HttpStatusCode.BadRequest
                ); 
            }
            
            // Add New User To Role: "User".
            var roleResult = await _userManager.AddToRoleAsync(newUser, "User");
            if (!roleResult.Succeeded)
            {
                _logger.LogWarning("Failed to assign 'User' role to {Email}: {Errors}", newUser.Email, string.Join(", ", roleResult.Errors.Select(e => e.Description)));
            }

            
            // Create New User.
            await _signInManager.SignInAsync(newUser, isPersistent:false);
            
            // Get The Generated Token From jwtService.
            var authenticationResponse = _tokenService.GenerateToken(newUser);
            
            // Return AuthenticationResponse.
            return authenticationResponse;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occurred while registering user with Email: {Email}", request.UserDto.Email);
            return AuthenticationResponse.Failure($"Error occurred while registering user with Email: {request.UserDto.Email}");
        }
    }
}