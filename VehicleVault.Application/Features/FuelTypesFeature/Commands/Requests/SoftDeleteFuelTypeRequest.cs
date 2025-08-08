using MediatR;
using VehicleVault.Application.Common;

namespace VehicleVault.Application.Features.FuelTypesFeature.Commands.Requests;

public record SoftDeleteFuelTypeRequest(int Id): IRequest<BaseResponse<Unit>>;