using VehicleVault.Domain.Common;

namespace VehicleVault.Domain.Entities;

public class Feature: BaseEntity
{
    public string Name { get; private set; } = null!;
    
    public bool IsDeleted { get; private set; } = false;
    
    public ICollection<VehicleFeature> VehicleFeatures { get; private set; } = new List<VehicleFeature>();

    private Feature() { }

    public Feature(string name, bool isDeleted)
    {
        Name = name;
        IsDeleted = isDeleted;
    }
}