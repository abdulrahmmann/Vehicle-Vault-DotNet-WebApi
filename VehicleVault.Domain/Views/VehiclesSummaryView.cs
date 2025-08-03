namespace VehicleVault.Domain.Views;

public class VehiclesSummaryView
{
    public int Id { get; init; }
    
    public string Name { get; init; } = null!;

    public short Year { get; init; }
    
    public string Engine { get; init; } = null!;

    public short EngineCc { get; init; }

    public byte EngineCylinders { get; init; }

    public decimal EngineLiterDisplay { get; init; }

    public byte NumDoors { get; init; }
    
    
    // Fields From Join Tables.
    
    public int BodyId { get; init; }
    public string BodyName  { get; init; } = null!;
    
    public int DriveTypeId { get; init; }
    public string DriveTypeName { get; init; } = null!;
    
    public int FuelTypeId  { get; init; }
    public string FuelTypeName { get; init; } = null!;
    
    public int MakeId  { get; init; }
    public string MakeName  { get; init; } = null!;
    
    public int ModelId  { get; init; }
    public string ModelName  { get; init; } = null!;
    
    public int SubModelId  { get; init; }
    public string SubModelName  { get; init; } = null!;
    
    public int TransmissionTypeId { get; init; }
    public string TransmissionTypeName { get; init; } = null!;
    
    public int ColorId { get; init; }
    public string ColorCode { get; init; } = null!;
    
    public int CategoryId { get; init; }
    public string CategoryName { get; init; } = null!;
    
    public int UserId { get; init; }
    public string UserName { get; init; } = null!;
}