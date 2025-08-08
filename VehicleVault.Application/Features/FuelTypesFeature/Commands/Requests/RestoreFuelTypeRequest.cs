using MediatR;
using VehicleVault.Application.Common;

namespace VehicleVault.Application.Features.FuelTypesFeature.Commands.Requests;

public record RestoreFuelTypeRequest(int Id): IRequest<BaseResponse<Unit>>;