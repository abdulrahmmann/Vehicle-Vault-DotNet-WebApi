namespace VehicleVault.Application.Features.VehiclesFeature.DTOs;

public record VehicleSummaryDto(
    int Id,
    string Name,
    short Year,
    string Engine,
    short EngineCc,
    byte EngineCylinders,
    decimal EngineLiterDisplay,
    byte NumDoors,
    string Description,

    int BodyId,
    string BodyName,

    int DriveTypeId,
    string DriveTypeName,

    int FuelTypeId,
    string FuelTypeName,

    int MakeId,
    string MakeName,

    int ModelId,
    string ModelName,

    int SubModelId,
    string SubModelName,

    int TransmissionTypeId,
    string TransmissionTypeName,

    int ColorId,
    string ColorCode,

    int CategoryId,
    string CategoryName,

    int UserId,
    string UserName
);
