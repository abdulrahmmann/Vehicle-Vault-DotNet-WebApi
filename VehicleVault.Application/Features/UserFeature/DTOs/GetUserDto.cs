namespace VehicleVault.Application.Features.UserFeature.DTOs;

public record GetUserDto(string Id, string? UserName, string? Email, string? PhoneNumber);