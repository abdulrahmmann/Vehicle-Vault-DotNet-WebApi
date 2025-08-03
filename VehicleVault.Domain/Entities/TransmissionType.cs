using VehicleVault.Domain.Common;

namespace VehicleVault.Domain.Entities;

public class TransmissionType: BaseEntity
{
    public string Name { get; init; } = null!;

    public ICollection<Vehicle> VehiclesCollection { get; init; } = new List<Vehicle>();
}

/*
 * Manual
 * Automatic
 * Semi-Automatic
 * Continuously Variable
 * Dual-Clutch 
 */