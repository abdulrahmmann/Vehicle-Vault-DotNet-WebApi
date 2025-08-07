using VehicleVault.Domain.Common;

namespace VehicleVault.Domain.Entities;

public class VehicleImage: BaseEntity
{
    public string ImageUrl { get; private set; } = null!;
    
    public bool IsPrimary { get; private set; }
    
    public bool IsDeleted { get; private set; } = false;
    
    
    // Vehicle & VehicleImage -> ONE_TO_MANY
    public int VehicleId { get; private set; }
    
    public Vehicle Vehicle { get; private set; } = null!;

    private VehicleImage() { }
}