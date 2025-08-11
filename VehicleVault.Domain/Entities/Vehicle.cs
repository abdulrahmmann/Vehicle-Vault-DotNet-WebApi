using VehicleVault.Domain.Common;
using VehicleVault.Domain.IdentityEntities;

namespace VehicleVault.Domain.Entities;

public class Vehicle: BaseEntity
{
    public string Name { get; private set; } = null!;

    public short Year { get; private set; }
    
    public string Engine { get; private set; } = null!;

    public short EngineCc { get; private set; }

    public byte EngineCylinders { get; private set; }

    public decimal EngineLiterDisplay { get; private set; }

    public byte NumDoors { get; private set; }
    
    public string Description { get; private set; } = null!;
    
    public bool IsDeleted { get; private set; } = false;
    
    
    // FOREIGN KEYS && NAVIGATIONS

    // Vehicle & Body -> ONE_TO_MANY
    public int BodyId { get; private set; }
    public Body Body { get; private set; } = null!;

    
    // Vehicle & DriveType -> ONE_TO_MANY
    public int DriveTypeId { get; private set; }
    public DriveType DriveType { get; private set; } = null!;

    
    // Vehicle & FuelType -> ONE_TO_MANY
    public int FuelTypeId { get; private set; }
    public FuelType FuelType { get; private set; } = null!;

    
    // Vehicle & Make -> ONE_TO_MANY 
    public int MakeId { get; private set; }
    public Make Make { get; private set; } = null!;

    
    // Vehicle & Model -> ONE_TO_MANY  
    public int ModelId { get; private set; }
    public Model Model { get; private set; } = null!;

    
    // Vehicle & SubModel -> ONE_TO_MANY 
    public int SubModelId { get; private set; }
    public SubModel SubModel { get; private set; } = null!;
    
    
    // Vehicle & Colors -> ONE_TO_MANY 
    public int ColorId { get; private set; }
    public Color Color { get; private set; } = null!;
    
    
    // Vehicle & Category -> ONE_TO_MANY 
    public int CategoryId { get; private set; }
    public Category Category { get; private set; } = null!;
    
    // Vehicle & TransmissionType -> ONE_TO_MANY 
    public int TransmissionTypeId { get; private set; }
    public TransmissionType  TransmissionType { get; private set; } =  null!;
    
    
    // Vehicle & User -> ONE_TO_MANY 
    public int UserId { get; private set; }
    public ApplicationUser User { get; private set; } = null!;
    
    public ICollection<VehicleFeature> VehicleFeatures { get; private set; } = new List<VehicleFeature>();
    public ICollection<VehicleImage> VehicleImages { get; private set; } = new List<VehicleImage>();
    
    private Vehicle(string description)
    {
        Description = description;
    }
    
    public Vehicle(string name, short year, string engine, short engineCc, byte engineCylinders, decimal engineLiterDisplay, 
        byte numDoors, bool isDeleted, int bodyId, int driveTypeId, int fuelTypeId, int makeId, int modelId, int subModelId, 
        int colorId, int categoryId, int transmissionTypeId, int userId, string description)
    {
        Name = name;
        Year = year;
        Engine = engine;
        EngineCc = engineCc;
        EngineCylinders = engineCylinders;
        EngineLiterDisplay = engineLiterDisplay;
        NumDoors = numDoors;
        IsDeleted = isDeleted;
        BodyId = bodyId;
        DriveTypeId = driveTypeId;
        FuelTypeId = fuelTypeId;
        MakeId = makeId;
        ModelId = modelId;
        SubModelId = subModelId;
        ColorId = colorId;
        CategoryId = categoryId;
        TransmissionTypeId = transmissionTypeId;
        UserId = userId;
        Description = description;
    }
    
    #region Update Vehicle
    public void UpdateVehicle(string name)
    {
        Name = name;
    }
    #endregion

    #region Soft Delete Vehicle
    public void SoftDeleteTransmissionType()
    {
        IsDeleted = true;
    }
    
    public void RestoreVehicle()
    {
        IsDeleted = false;
    }
    #endregion
}