namespace VehicleVault.Application.Features.VehiclesFeature.DTOs;

public record CreateVehicleDto(
    string Name,
    short Year,
    string Engine,
    short EngineCc,
    byte EngineCylinders,
    decimal EngineLiterDisplay,
    byte NumDoors,
    string Description,

    int BodyId,
    int DriveTypeId,
    int FuelTypeId,
    int MakeId,
    int ModelId,
    int SubModelId,
    int TransmissionTypeId,
    int ColorId,
    int CategoryId,
    int UserId
);