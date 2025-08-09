using VehicleVault.Domain.Common;

namespace VehicleVault.Domain.Entities;

public class TransmissionType: BaseEntity
{
    public string Name { get; private set; } = null!;
    
    public bool IsDeleted { get; private set; } = false;

    public ICollection<Vehicle> VehiclesCollection { get; private set; } = new List<Vehicle>();

    private TransmissionType() { }

    public TransmissionType(string name)
    {
        Name = name;
    }
    
    #region Update TransmissionType
    public void UpdateTransmissionType(string name)
    {
        Name = name;
    }
    #endregion

    #region Soft Delete TransmissionType
    public void SoftDeleteTransmissionType()
    {
        IsDeleted = true;
    }
    
    public void RestoreTransmissionType()
    {
        IsDeleted = false;
    }
    #endregion
}

/*
 * Manual
 * Automatic
 * Semi-Automatic
 * Continuously Variable
 * Dual-Clutch 
 */