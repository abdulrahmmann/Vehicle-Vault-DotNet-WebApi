using VehicleVault.Domain.Common;

namespace VehicleVault.Domain.Entities;

public class SubModel: BaseEntity
{
    public string Name { get; private set; } = null!;
    
    public bool IsDeleted { get; private set; } = false;
    
    // FOREIGN KEYS && NAVIGATIONS
    
    // Models & SubModels -> ONE_TO_MANY 
    public int ModelId { get; private set; }
    public Model Model { get; private set; } = null!;
    
    // Vehicle & SubModel -> ONE_TO_MANY
    public ICollection<Vehicle> VehiclesCollection { get; private set; }  = new List<Vehicle>();

    private SubModel() { }

    public SubModel(string name, int modelId)
    {
        Name = name;
        ModelId = modelId;
    }
}