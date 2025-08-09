using MediatR;
using VehicleVault.Application.Common;

namespace VehicleVault.Application.Features.DriveTypesFeature.Commands.Requests;

public record RestoreDriveTypeRequest(int Id): IRequest<BaseResponse<Unit>>;