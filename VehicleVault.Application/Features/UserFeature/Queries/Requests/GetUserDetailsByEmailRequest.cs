using MediatR;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.UserFeature.DTOs;

namespace VehicleVault.Application.Features.UserFeature.Queries.Requests;

public record GetUserDetailsByEmailRequest(string Email): IRequest<UserResponse<GetUserDetailsDto>>;