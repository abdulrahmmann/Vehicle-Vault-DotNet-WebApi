using MediatR;
using VehicleVault.Application.Common;

namespace VehicleVault.Application.Features.DriveTypesFeature.Commands.Requests;

public record SoftDeleteDriveTypeRequest(int Id): IRequest<BaseResponse<Unit>>;