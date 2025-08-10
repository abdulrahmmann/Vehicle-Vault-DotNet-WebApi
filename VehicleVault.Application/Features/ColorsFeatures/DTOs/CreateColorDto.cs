namespace VehicleVault.Application.Features.ColorsFeatures.DTOs;

public record CreateColorDto(string Name, string Code, string FinishType, bool IsActive);