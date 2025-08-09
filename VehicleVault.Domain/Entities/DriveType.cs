using VehicleVault.Domain.Common;

namespace VehicleVault.Domain.Entities;

public class DriveType: BaseEntity
{
    public string Name { get; private set; } = null!;
    
    public bool IsDeleted { get; private set; } = false;
    
    // FOREIGN KEYS && NAVIGATIONS
    
    // Vehicle & DriveType -> ONE_TO_MANY
    public ICollection<Vehicle> VehiclesCollection { get; private set; } = new List<Vehicle>();

    private DriveType() { }

    public DriveType(string name)
    {
        Name = name;
    }
    
    #region Update DriveType
    public void UpdateDriveType(string name)
    {
        this.Name = name;
    }
    #endregion
    
    #region Soft Delete DriveType
    public void SoftDeleteDriveType()
    {
        this.IsDeleted = true;
    }
    
    public void RestoreDriveType()
    {
        this.IsDeleted = false;
    }
    #endregion
}