using MediatR;
using VehicleVault.Application.Common;

namespace VehicleVault.Application.Features.UserFeature.Commands.Requests;

public record DeleteUserRequest(string UserEmail): IRequest<BaseResponse<Unit>>;