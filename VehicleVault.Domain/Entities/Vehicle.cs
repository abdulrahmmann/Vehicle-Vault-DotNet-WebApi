using VehicleVault.Domain.Common;
using VehicleVault.Domain.IdentityEntities;

namespace VehicleVault.Domain.Entities;

public class Vehicle: BaseEntity
{
    public string Name { get; init; } = null!;

    public short Year { get; init; }
    
    public string Engine { get; init; } = null!;

    public short EngineCc { get; init; }

    public byte EngineCylinders { get; init; }

    public decimal EngineLiterDisplay { get; init; }

    public byte NumDoors { get; init; }
    
    
    // FOREIGN KEYS && NAVIGATIONS

    // Vehicle & Body -> ONE_TO_MANY
    public int BodyId { get; init; }
    public Body Body { get; init; } = null!;

    
    // Vehicle & DriveType -> ONE_TO_MANY
    public int DriveTypeId { get; init; }
    public DriveType DriveType { get; init; } = null!;

    
    // Vehicle & FuelType -> ONE_TO_MANY
    public int FuelTypeId { get; init; }
    public FuelType FuelType { get; init; } = null!;

    
    // Vehicle & Make -> ONE_TO_MANY 
    public int MakeId { get; init; }
    public Make Make { get; init; } = null!;

    
    // Vehicle & Model -> ONE_TO_MANY  
    public int ModelId { get; init; }
    public Model Model { get; init; } = null!;

    
    // Vehicle & SubModel -> ONE_TO_MANY 
    public int SubModelId { get; init; }
    public SubModel SubModel { get; init; } = null!;
    
    
    // Vehicle & Colors -> ONE_TO_MANY 
    public int ColorId { get; init; }
    public Color Color { get; init; } = null!;
    
    
    // Vehicle & Category -> ONE_TO_MANY 
    public int CategoryId { get; init; }
    public Category Category { get; init; } = null!;
    
    // Vehicle & TransmissionType -> ONE_TO_MANY 
    public int TransmissionTypeId { get; init; }
    public TransmissionType  TransmissionType { get; init; } =  null!;
    
    
    // Vehicle & User -> ONE_TO_MANY 
    public int UserId { get; init; }
    public ApplicationUser User { get; init; } = null!;
    
    public ICollection<VehicleFeature> VehicleFeatures { get; init; } = new List<VehicleFeature>();
    public ICollection<VehicleImage> VehicleImages { get; init; } = new List<VehicleImage>();
}