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
}

/*
 * Manual
 * Automatic
 * Semi-Automatic
 * Continuously Variable
 * Dual-Clutch 
 */