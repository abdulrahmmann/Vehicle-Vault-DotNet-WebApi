namespace VehicleVault.Application.Features.UserFeature.DTOs;

public record RegisterUserDtoByAdmin
    (string UserName, string Email, string PhoneNumber, string Password, string ConfirmPassword, string Role);