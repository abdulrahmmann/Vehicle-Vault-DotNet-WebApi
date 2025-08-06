using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.UserFeature.Commands.Requests;
using VehicleVault.Application.Features.UserFeature.DTOs;
using VehicleVault.Application.Services.Tokens.GenerateToken;
using VehicleVault.Domain.IdentityEntities;

namespace VehicleVault.Application.Features.UserFeature.Commands.Handler;

public class UpdateUserHandler: IRequestHandler<UpdateUserRequest, AuthenticationResponse>
{
    #region Instance Fields
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IGenerateTokenService _tokenService;
    private readonly IValidator<UpdateUserDto>  _validator;
    private readonly ILogger<UpdateUserHandler>  _logger;
    #endregion

    #region Constructor
    public UpdateUserHandler(UserManager<ApplicationUser> userManager, IGenerateTokenService tokenService, ILogger<UpdateUserHandler> logger, IValidator<UpdateUserDto> validator)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _logger = logger;
        _validator = validator;
    }
    #endregion

    public async Task<AuthenticationResponse> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var userRequest = request.UserDto;
            
            var validationResult = await _validator.ValidateAsync(userRequest, cancellationToken);
            
            if (!validationResult.IsValid)
            {
                var errors = string.Join(",", validationResult.Errors.Select(e => e.ErrorMessage));
                
                _logger.LogError("Errors: {errors}", errors);
                return AuthenticationResponse.Failure("Invalid request");
            }
            
            var user = await _userManager.FindByEmailAsync(request.email);
            
            if (user == null) return AuthenticationResponse.Failure("User not found");

            user.Email = userRequest.Email ?? user.Email;
            user.UserName = userRequest.UserName ?? user.UserName;
            user.PhoneNumber = userRequest.PhoneNumber ?? user.PhoneNumber;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                var errors = string.Join(",", result.Errors.Select(e => e.Description));
                _logger.LogError("Failed to update user: {errors}", errors);
                return AuthenticationResponse.Failure($"Failed to update user, error: {errors}");
            }

            var tokenResponse = _tokenService.GenerateToken(user);
            user.RefreshToken = tokenResponse.RefreshToken;
            user.RefreshTokenExpiration =  tokenResponse.RefreshTokenExpiration;
            
            await _userManager.UpdateAsync(user);

            return tokenResponse;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Unexpected error while Updating user: {Email}", request.UserDto.Email); 
            return AuthenticationResponse.Failure("Unexpected server error. Please try again later."); 
        }
    }
}