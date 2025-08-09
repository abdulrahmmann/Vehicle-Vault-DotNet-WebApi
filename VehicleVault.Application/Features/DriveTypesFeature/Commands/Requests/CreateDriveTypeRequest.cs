using MediatR;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.DriveTypesFeature.DTOs;

namespace VehicleVault.Application.Features.DriveTypesFeature.Commands.Requests;

public record CreateDriveTypeRequest(CreateDriveTypeDto DriveTypeDto): IRequest<BaseResponse<Unit>>;