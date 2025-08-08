using MediatR;
using VehicleVault.Application.Common;

namespace VehicleVault.Application.Features.BodyFeature.Commands.Requests;

public record SoftDeleteBodyRequest(int Id): IRequest<BaseResponse<Unit>>;