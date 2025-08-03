using VehicleVault.Domain.Common;

namespace VehicleVault.Domain.Entities;

public class Color: BaseEntity
{
    public string Name { get; init; } = null!;
    
    // Vehicle & Color -> ONE_TO_MANY
    public ICollection<Vehicle> VehiclesCollection { get; init; }  = new List<Vehicle>();
}