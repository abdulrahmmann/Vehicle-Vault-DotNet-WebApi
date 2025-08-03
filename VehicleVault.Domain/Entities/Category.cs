using VehicleVault.Domain.Common;

namespace VehicleVault.Domain.Entities;

public class Category: BaseEntity
{
    public string Name { get; init; } = null!;
    
    // Vehicle & Category -> ONE_TO_MANY
    public ICollection<Vehicle> VehiclesCollection { get; init; }  = new List<Vehicle>();
}