using VehicleVault.Domain.Common;

namespace VehicleVault.Domain.Entities;

public class Body: BaseEntity
{
    public string Name { get; init; } = null!;
    
    // FOREIGN KEYS && NAVIGATIONS
    
    // Vehicle & Body -> ONE_TO_MANY
    public ICollection<Vehicle> VehiclesCollection { get; init; } = new List<Vehicle>();
}