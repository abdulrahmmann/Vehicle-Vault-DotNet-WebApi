using MediatR;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.DriveTypesFeature.DTOs;

namespace VehicleVault.Application.Features.DriveTypesFeature.Queries.Requests;

public record GetDriveTypeByNameRequest(string Name): IRequest<BaseResponse<DriveTypeDto>>;