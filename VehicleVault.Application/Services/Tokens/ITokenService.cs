using VehicleVault.Application.Common;
using VehicleVault.Domain.IdentityEntities;

namespace VehicleVault.Application.Services.Tokens;

public interface ITokenService
{
    AuthenticationResponse GenerateToken(ApplicationUser user);
    
    string GenerateRefreshToken();
}