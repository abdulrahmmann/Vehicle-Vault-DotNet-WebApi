using VehicleVault.Domain.Common;

namespace VehicleVault.Domain.Entities;

public class Model: BaseEntity
{
    public string Name { get; init; } = null!;
    
    // FOREIGN KEYS && NAVIGATIONS
    
    // Makes & Model -> ONE_TO_MANY 
    public int MakeId { get; init; }
    public Make Make { get; init; } = null!;
    
    // Model & SubModels -> ONE_TO_MANY 
    public ICollection<SubModel> SubModelsCollection { get; init; } = new  List<SubModel>();
    
    // Vehicle & Model -> ONE_TO_MANY
    public ICollection<Vehicle> VehiclesCollection { get; init; }  = new List<Vehicle>();
}

