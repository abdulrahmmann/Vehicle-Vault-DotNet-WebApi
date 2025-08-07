using VehicleVault.Domain.Common;

namespace VehicleVault.Domain.Entities;

public class FuelType: BaseEntity
{
    public string Name { get; private set; } = null!;
    
    public bool IsDeleted { get; private set; } = false;
    
    // FOREIGN KEYS && NAVIGATIONS
    
    // Vehicle & FuelType -> ONE_TO_MANY
    public ICollection<Vehicle> VehiclesCollection { get; private set; } = new List<Vehicle>();

    private FuelType() { }

    public FuelType(string name, bool isDeleted)
    {
        Name = name;
        IsDeleted = isDeleted;
    }
}