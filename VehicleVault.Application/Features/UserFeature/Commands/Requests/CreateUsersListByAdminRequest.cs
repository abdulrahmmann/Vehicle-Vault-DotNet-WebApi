using MediatR;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.UserFeature.DTOs;

namespace VehicleVault.Application.Features.UserFeature.Commands.Requests;

public record CreateUsersListByAdminRequest(IEnumerable<CreateUserDto> UsersDto): IRequest<UserResponse<Unit>>;