using VehicleVault.Domain.Common;

namespace VehicleVault.Domain.Entities;

public class Body: BaseEntity
{
    public string Name { get; private set; } = null!;
    
    public bool IsDeleted { get; private set; } = false;
    
    // FOREIGN KEYS && NAVIGATIONS
    
    // Vehicle & Body -> ONE_TO_MANY
    public ICollection<Vehicle> VehiclesCollection { get; private set; } = new List<Vehicle>();

    private Body() { }

    public Body(string name, bool isDeleted)
    {
        Name = name;
        IsDeleted = isDeleted;
    }
    
    #region Update Body
    public void UpdateBody(string name)
    {
        this.Name = name;
    }
    #endregion

    #region Soft Delete Body
    public void SoftDeleteBody()
    {
        this.IsDeleted = true;
    }
    
    public void RestoreBody()
    {
        this.IsDeleted = false;
    }
    #endregion
}