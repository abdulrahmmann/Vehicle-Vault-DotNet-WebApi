using System.Security.Cryptography;

namespace VehicleVault.Application.Services.Tokens.GenerateRefreshToken;

public class GenerateRefreshTokenService: IGenerateRefreshTokenService
{
    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];

        using var randomNumberGenerator = RandomNumberGenerator.Create();
        
        randomNumberGenerator.GetBytes(randomNumber);

        return Convert.ToBase64String(randomNumber);
    }
}