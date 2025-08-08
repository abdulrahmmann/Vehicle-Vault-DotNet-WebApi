using MediatR;
using VehicleVault.Application.Common;

namespace VehicleVault.Application.Features.MakesFeature.Commands.Requests;

public record DeleteMakeRequest(int Id): IRequest<BaseResponse<Unit>>;