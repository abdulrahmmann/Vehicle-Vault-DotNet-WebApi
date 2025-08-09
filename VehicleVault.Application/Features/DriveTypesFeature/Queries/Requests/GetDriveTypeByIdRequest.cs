using MediatR;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.DriveTypesFeature.DTOs;

namespace VehicleVault.Application.Features.DriveTypesFeature.Queries.Requests;

public record GetDriveTypeByIdRequest(int Id): IRequest<BaseResponse<DriveTypeDto>>;