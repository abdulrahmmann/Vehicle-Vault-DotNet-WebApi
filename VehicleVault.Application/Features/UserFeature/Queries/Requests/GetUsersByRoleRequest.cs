using MediatR;
using VehicleVault.Application.Common;
using VehicleVault.Application.Constants;
using VehicleVault.Application.Features.UserFeature.DTOs;

namespace VehicleVault.Application.Features.UserFeature.Queries.Requests;

public record GetUsersByRoleRequest(Roles Role): IRequest<IEnumerable<UserResponse<GetUserDto>>>;