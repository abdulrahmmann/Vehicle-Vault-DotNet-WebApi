using MediatR;
using VehicleVault.Application.Common;

namespace VehicleVault.Application.Features.MakesFeature.Commands.Requests;

public record SoftDeleteMakeRequest(int Id): IRequest<BaseResponse<Unit>>;