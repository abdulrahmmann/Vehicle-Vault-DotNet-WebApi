namespace VehicleVault.Domain.Entities;

public class VehicleFeature
{
    public Vehicle Vehicle { get; private set; } = null!;

    public int VehicleId { get; private set; }
    public int FeatureId { get; private set; }
    
    public bool IsDeleted { get; private set; } = false;
    
    public Feature Feature { get; private set; } = null!;

    private VehicleFeature() { }
}