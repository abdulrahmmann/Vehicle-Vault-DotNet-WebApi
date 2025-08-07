using VehicleVault.Domain.Common;

namespace VehicleVault.Domain.Entities;

public class Color: BaseEntity
{
    public string Name { get; private set; } = null!;
    
    public bool IsDeleted { get; private set; } = false;
    
    // Vehicle & Color -> ONE_TO_MANY
    public ICollection<Vehicle> VehiclesCollection { get; private set; }  = new List<Vehicle>();

    private Color() { }
    
    public Color(string name, bool isDeleted)
    {
        Name = name;
        IsDeleted = isDeleted;
    }
}