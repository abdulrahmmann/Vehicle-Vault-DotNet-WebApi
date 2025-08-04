using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.UserFeature.Commands.Requests;
using VehicleVault.Application.Services.Tokens.GeneratePrincipalJwtToken;
using VehicleVault.Application.Services.Tokens.GenerateToken;
using VehicleVault.Domain.IdentityEntities;

namespace VehicleVault.Application.Features.UserFeature.Commands.Handler;

public class GenerateNewAccessTokenHandler: IRequestHandler<GenerateNewAccessTokenRequest, AuthenticationResponse>
{
    #region Instance Fields
    private readonly UserManager<ApplicationUser>  _userManager;
    private readonly IGenerateTokenService  _generateTokenService;
    private readonly IGeneratePrincipalFromJwtTokenService _generatePrincipalTokenService;
    private readonly ILogger<GenerateNewAccessTokenHandler> _logger;
    #endregion
    
    #region Constructor
    public GenerateNewAccessTokenHandler(UserManager<ApplicationUser> userManager, IGenerateTokenService generateTokenService, 
        IGeneratePrincipalFromJwtTokenService generatePrincipalTokenService, ILogger<GenerateNewAccessTokenHandler> logger)
    {
        _userManager = userManager;
        _generateTokenService = generateTokenService;
        _generatePrincipalTokenService = generatePrincipalTokenService;
        _logger = logger;
    }
    #endregion
    
    
    public async Task<AuthenticationResponse> Handle(GenerateNewAccessTokenRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var tokeRequest = request.TokenModel;
            
            if (tokeRequest.Token == null) return AuthenticationResponse.Failure("Invalid Token Request");

            var principal = _generatePrincipalTokenService.GetPrincipalFromJwtToken(tokeRequest.Token);
            
            if (principal == null) return AuthenticationResponse.Failure("Invalid Token Request");

            var email = principal.FindFirstValue(ClaimTypes.NameIdentifier);

            if (email == null) return AuthenticationResponse.Failure("Email Is Null");
            
            var user = await _userManager.FindByEmailAsync(email);
            
            if (user == null || user.RefreshToken != tokeRequest.RefreshToken || user.RefreshTokenExpiration <= DateTime.Now) 
                return AuthenticationResponse.Failure("Invalid Refresh Token");

            var authenticationResponse = _generateTokenService.GenerateToken(user);

            user.RefreshToken = authenticationResponse.RefreshToken;
            user.RefreshTokenExpiration = authenticationResponse.RefreshTokenExpiration;

            await _userManager.UpdateAsync(user);

            return authenticationResponse;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Unexpected error while Generate Principal Token Service: {Email}", request.TokenModel.Token); 
            return AuthenticationResponse.Failure("Unexpected server error. Please try again later."); 
        }
    }
}