namespace VehicleVault.Application.Features.UserFeature.DTOs;

public record RegisterUserDto(string UserName, string Email, string PhoneNumber, string Password, string ConfirmPassword);