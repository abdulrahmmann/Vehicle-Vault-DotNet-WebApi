using VehicleVault.Domain.Common;

namespace VehicleVault.Domain.Entities;

public class SubModel: BaseEntity
{
    public string Name { get; init; } = null!;
    
    // FOREIGN KEYS && NAVIGATIONS
    
    // Models & SubModels -> ONE_TO_MANY 
    public int ModelId { get; init; }
    public Model Model { get; init; } = null!;
    
    // Vehicle & SubModel -> ONE_TO_MANY
    public ICollection<Vehicle> VehiclesCollection { get; init; }  = new List<Vehicle>();
}