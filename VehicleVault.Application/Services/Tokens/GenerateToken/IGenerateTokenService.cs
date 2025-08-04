using VehicleVault.Application.Common;
using VehicleVault.Domain.IdentityEntities;

namespace VehicleVault.Application.Services.Tokens.GenerateToken;

public interface IGenerateTokenService
{
    AuthenticationResponse GenerateToken(ApplicationUser user);
}