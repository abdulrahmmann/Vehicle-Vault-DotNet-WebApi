using VehicleVault.Domain.Common;

namespace VehicleVault.Domain.Entities;

public class VehicleFeature
{
    public Vehicle Vehicle { get; init; } = null!;

    public int VehicleId { get; init; }
    public int FeatureId { get; init; }
    
    public Feature Feature { get; init; } = null!;
}