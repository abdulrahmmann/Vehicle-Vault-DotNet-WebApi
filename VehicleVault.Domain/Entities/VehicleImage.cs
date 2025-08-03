using VehicleVault.Domain.Common;

namespace VehicleVault.Domain.Entities;

public class VehicleImage: BaseEntity
{
    public string ImageUrl { get; init; } = null!;
    
    public bool IsPrimary { get; init; }
    
    
    // Vehicle & VehicleImage -> ONE_TO_MANY
    public int VehicleId { get; init; }
    
    public Vehicle Vehicle { get; init; } = null!;
}