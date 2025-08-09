using MediatR;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.DriveTypesFeature.DTOs;

namespace VehicleVault.Application.Features.DriveTypesFeature.Commands.Requests;

public record UpdateDriveTypeRequest(int Id, UpdateDriveTypeDto DriveTypeDto): IRequest<BaseResponse<Unit>>;