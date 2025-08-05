using MediatR;
using VehicleVault.Application.Common;
using VehicleVault.Application.Constants;
using VehicleVault.Application.Features.UserFeature.DTOs;

namespace VehicleVault.Application.Features.UserFeature.Queries.Requests;

public record GetUsersByRoleRequest(string Role): IRequest<UserResponse<IEnumerable<GetUserDto>>>;