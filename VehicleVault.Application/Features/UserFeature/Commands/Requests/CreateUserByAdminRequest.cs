using MediatR;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.UserFeature.DTOs;

namespace VehicleVault.Application.Features.UserFeature.Commands.Requests;

public record CreateUserByAdminRequest(RegisterUserDtoByAdmin UserDtoByAdmin): IRequest<UserResponse<Unit>>;