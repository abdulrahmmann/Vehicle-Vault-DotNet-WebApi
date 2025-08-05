using VehicleVault.Application.Constants;

namespace VehicleVault.Application.Features.UserFeature.DTOs;

public record CreateUserDto(string UserName, string Email, string PhoneNumber, string Password, string Role);