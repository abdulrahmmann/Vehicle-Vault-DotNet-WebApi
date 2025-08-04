using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using VehicleVault.Application.Common;
using VehicleVault.Domain.IdentityEntities;

namespace VehicleVault.Application.Services.Tokens.GenerateToken;

public class GenerateTokenService: IGenerateTokenService
{
    #region INSTANCEs FIELDS
    private readonly IConfiguration _configuration;
    #endregion

    #region CONSTRUCTOR
    public GenerateTokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    #endregion
    
    public AuthenticationResponse GenerateToken(ApplicationUser user)
    {
        //Token Expiration Minutes.
        var expiration = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:EXPIRATION_MINUTES"]));

        // Create Claims.
        var claims = new Claim[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, expiration.ToString(CultureInfo.InvariantCulture)),
            
            new Claim(ClaimTypes.NameIdentifier, user.Email!),
            new Claim(ClaimTypes.Name, user.UserName!),
        };
        
        // Get The Secret Key.
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SECRET_KEY"]!));

        // Create Signing Credentials.
        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        // Generate Token.
        var tokenGenerator = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: expiration,
            signingCredentials: signinCredentials
        );

        // Write Token.
        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.WriteToken(tokenGenerator);

        // Return AuthenticationResponse.
        return AuthenticationResponse.Success(
            username: user.UserName!,
            email: user.Email!,
            token: token,
            expiration: expiration,
            message: "Token Generated Successfully"
        );
    }
}