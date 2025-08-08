using VehicleVault.Domain.Common;

namespace VehicleVault.Domain.Entities;

public class Make : BaseEntity
{
    public string Name { get; private set; } = null!;
    
    public bool IsDeleted { get; private set; } = false;
    
    // FOREIGN KEYS && NAVIGATIONS
    
    // Makes & Model -> ONE_TO_MANY 
    public ICollection<Model> ModelsCollection { get; private set; } = new List<Model>();
    
    // Vehicle & Makes -> ONE_TO_MANY
    public ICollection<Vehicle> VehiclesCollection { get; private set; }  = new List<Vehicle>();

    private Make() { }

    public Make(string name)
    {
        Name = name;
    }
    
    #region Update Category
    public void UpdateMake(string name)
    {
        this.Name = name;
    }
    #endregion

    #region Soft Delete Category
    public void SoftDeleteMake()
    {
        this.IsDeleted = true;
    }
    
    public void RestoreMake()
    {
        this.IsDeleted = false;
    }
    #endregion
}
