using VehicleVault.Domain.Common;

namespace VehicleVault.Domain.Entities;

public class Color: BaseEntity
{
    public string Name { get; private set; } = null!;
    
    public string Code { get; private set; } = null!;
    
    public string FinishType { get;  set; } = null!;
    
    public bool IsActive { get; private set; } 
    
    public bool IsDeleted { get; private set; } = false;
    
    // Vehicle & Color -> ONE_TO_MANY
    public ICollection<Vehicle> VehiclesCollection { get; private set; }  = new List<Vehicle>();

    private Color() { }

    public Color(string name, string code, string finishType, bool isActive)
    {
        Name = name;
        Code = code;
        FinishType = finishType;
        IsActive = isActive;
    }
    
    #region Update Color
    public void UpdateColor(string name, string code, string finishType, bool isActive)
    {
        Name = name;
        Code = code;
        FinishType = finishType;
        IsActive = isActive;
    }
    #endregion

    #region Soft Delete Category
    public void SoftDeleteColor()
    {
        this.IsDeleted = true;
    }
    
    public void RestoreColor()
    {
        this.IsDeleted = false;
    }
    #endregion
}