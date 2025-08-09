using VehicleVault.Domain.Common;

namespace VehicleVault.Domain.Entities;

public class Model: BaseEntity
{
    public string Name { get; private set; } = null!;
    
    public bool IsDeleted { get; private set; } = false;
    
    // FOREIGN KEYS && NAVIGATIONS
    
    // Makes & Model -> ONE_TO_MANY 
    public int MakeId { get; private set; }
    public Make Make { get; private set; } = null!;
    
    // Model & SubModels -> ONE_TO_MANY 
    public ICollection<SubModel> SubModelsCollection { get; private set; } = new  List<SubModel>();
    
    // Vehicle & Model -> ONE_TO_MANY
    public ICollection<Vehicle> VehiclesCollection { get; private set; }  = new List<Vehicle>();

    private Model() { }

    public Model(string name)
    {
        Name = name;
    }

    public Model(string name, int makeId)
    {
        Name = name;
        MakeId = makeId;
    }

    #region Update Model
    public void UpdateModel(string name)
    {
        Name = name;
    }
    #endregion

    #region Soft Delete Model
    public void SoftDeleteModel()
    {
        IsDeleted = true;
    }
    
    public void RestoreModel()
    {
        IsDeleted = false;
    }
    #endregion
}

