using MediatR;
using VehicleVault.Application.Common;

namespace VehicleVault.Application.Features.BodyFeature.Commands.Requests;

public record RestoreBodyRequest(int Id): IRequest<BaseResponse<Unit>>;