using System.Security.Claims;

namespace VehicleVault.Application.Services.Tokens.GeneratePrincipalJwtToken;

public interface IGeneratePrincipalFromJwtTokenService
{
    ClaimsPrincipal GetPrincipalFromJwtToken(string token);
}