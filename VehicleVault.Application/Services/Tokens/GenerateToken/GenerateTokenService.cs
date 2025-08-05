using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using VehicleVault.Application.Common;
using VehicleVault.Application.Services.Tokens.GenerateRefreshToken;
using VehicleVault.Domain.IdentityEntities;

namespace VehicleVault.Application.Services.Tokens.GenerateToken;

public class GenerateTokenService: IGenerateTokenService
{
    #region INSTANCEs FIELDS
    private readonly IConfiguration _configuration;
    private readonly IGenerateRefreshTokenService _refreshTokenService;
    private readonly UserManager<ApplicationUser>  _userManager;
    #endregion

    #region CONSTRUCTOR
    public GenerateTokenService(IConfiguration configuration, IGenerateRefreshTokenService refreshTokenService, 
        UserManager<ApplicationUser> userManager)
    {
        _configuration = configuration;
        _refreshTokenService = refreshTokenService;
        _userManager = userManager;
    }
    #endregion
    
    public AuthenticationResponse GenerateToken(ApplicationUser user)
    {
        //Token Expiration Minutes.
        var expiration = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:EXPIRATION_MINUTES"]));

        // Get User Role.
        var roles = _userManager.GetRolesAsync(user).Result;
        var role = roles.FirstOrDefault();
        
        // Create Claims.
        var claims = new Claim[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            //new Claim(JwtRegisteredClaimNames.Iat, expiration.ToString(CultureInfo.InvariantCulture)),
            new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),

            new Claim(ClaimTypes.NameIdentifier, user.Email!),
            new Claim(ClaimTypes.Name, user.UserName!),
            
            new Claim(ClaimTypes.Role, role),
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
            refreshToken: _refreshTokenService.GenerateRefreshToken(),
            refreshTokenExpiration: DateTime.Now.AddMinutes(Convert.ToInt64(_configuration["RefreshToken:EXPIRATION_MINUTES"])),
            expiration: expiration,
            message: "Token Generated Successfully"
        );
    }
}