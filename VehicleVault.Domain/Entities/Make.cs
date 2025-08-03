using VehicleVault.Domain.Common;

namespace VehicleVault.Domain.Entities;

public class Make : BaseEntity
{
    public string Name { get; init; } = null!;
    
    // FOREIGN KEYS && NAVIGATIONS
    
    // Makes & Model -> ONE_TO_MANY 
    public ICollection<Model> ModelsCollection { get; init; } = new List<Model>();
    
    // Vehicle & Makes -> ONE_TO_MANY
    public ICollection<Vehicle> VehiclesCollection { get; init; }  = new List<Vehicle>();
}
