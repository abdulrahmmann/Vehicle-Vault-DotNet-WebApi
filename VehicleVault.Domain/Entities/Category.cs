using VehicleVault.Domain.Common;

namespace VehicleVault.Domain.Entities;

public class Category: BaseEntity
{
    public string Name { get; private set; } = null!;
    
    public string Description { get; private set; } = null!;
    
    public bool IsDeleted { get; private set; } = false;
    
    // Vehicle & Category -> ONE_TO_MANY
    public ICollection<Vehicle> VehiclesCollection { get; private set; }  = new List<Vehicle>();

    // Required by EF Core.
    private Category() {}
    
    public Category(string name, string description)
    {
        Name = name;
        Description = description;
    }

    #region Update Category
    public void UpdateCategory(string name, string description)
    {
        this.Name = name;
        this.Description = description;
    }
    #endregion

    #region Soft Delete Category

    public void SoftDeleteCategory()
    {
        this.IsDeleted = true;
    }
    
    public void RestoreCategory()
    {
        this.IsDeleted = false;
    }

    #endregion
}