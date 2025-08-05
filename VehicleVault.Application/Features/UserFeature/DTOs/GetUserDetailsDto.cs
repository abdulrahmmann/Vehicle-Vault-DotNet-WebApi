namespace VehicleVault.Application.Features.UserFeature.DTOs;

public record GetUserDetailsDto(string Id, string UserName, string? Email, string? PhoneNumber,
    string? RefreshToken, DateTime? RefreshTokenExpiration);