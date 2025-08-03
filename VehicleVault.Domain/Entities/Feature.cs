using VehicleVault.Domain.Common;

namespace VehicleVault.Domain.Entities;

public class Feature: BaseEntity
{
    public string Name { get; init; } = null!;
    
    public ICollection<VehicleFeature> VehicleFeatures { get; init; } = new List<VehicleFeature>();

}