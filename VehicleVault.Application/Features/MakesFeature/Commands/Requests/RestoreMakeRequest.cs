using MediatR;
using VehicleVault.Application.Common;

namespace VehicleVault.Application.Features.MakesFeature.Commands.Requests;

public record RestoreMakeRequest(int Id): IRequest<BaseResponse<Unit>>;