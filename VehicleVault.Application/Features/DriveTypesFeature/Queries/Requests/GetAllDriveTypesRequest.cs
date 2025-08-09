using MediatR;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.DriveTypesFeature.DTOs;

namespace VehicleVault.Application.Features.DriveTypesFeature.Queries.Requests;

public record GetAllDriveTypesRequest(): IRequest<BaseResponse<IEnumerable<DriveTypeDto>>>;