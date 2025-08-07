using VehicleVault.Domain.Common;

namespace VehicleVault.Domain.Entities;

public class Body: BaseEntity
{
    public string Name { get; private set; } = null!;
    
    public bool IsDeleted { get; private set; } = false;
    
    // FOREIGN KEYS && NAVIGATIONS
    
    // Vehicle & Body -> ONE_TO_MANY
    public ICollection<Vehicle> VehiclesCollection { get; private set; } = new List<Vehicle>();

    private Body() { }

    public Body(string name, bool isDeleted)
    {
        Name = name;
        IsDeleted = isDeleted;
    }
}