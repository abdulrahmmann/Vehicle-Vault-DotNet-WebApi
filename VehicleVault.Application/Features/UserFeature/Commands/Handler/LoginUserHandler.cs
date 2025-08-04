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

public class LoginUserHandler: IRequestHandler<LoginUserRequest, AuthenticationResponse>
{
    #region Instance Fields
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly IValidator<LoginUserDto> _validator;
    private readonly ITokenService _tokenService;
    private readonly ILogger<LoginUserHandler> _logger;
    #endregion

    #region Constructor
    public LoginUserHandler(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, 
        RoleManager<ApplicationRole> roleManager, IValidator<LoginUserDto> validator, ITokenService tokenService, 
        ILogger<LoginUserHandler> logger)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _validator = validator;
        _tokenService = tokenService;
        _logger = logger;
    }
    #endregion
    
    public async Task<AuthenticationResponse> Handle(LoginUserRequest request, CancellationToken cancellationToken)
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
                return AuthenticationResponse.Failure("Validation failed.", validationErrors, HttpStatusCode.UnprocessableEntity);
            }
            
            // 2. Check if email exists
            var user = await _userManager.FindByEmailAsync(userRequest.Email);
            
            if (user is null)
            {
                return AuthenticationResponse.Failure("Email Does not exists.", statusCode: HttpStatusCode.NotFound);
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, userRequest.Password, lockoutOnFailure:false);

            if (!result.Succeeded)
            {
                return AuthenticationResponse.Failure("Invalid login attempt.", statusCode: HttpStatusCode.Unauthorized);
            }

            var tokenResponse = _tokenService.GenerateToken(user);

            return tokenResponse;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Unexpected error while registering user: {Email}", request.UserDto.Email); 
            return AuthenticationResponse.Failure("Unexpected server error. Please try again later."); 
        }
    }
}