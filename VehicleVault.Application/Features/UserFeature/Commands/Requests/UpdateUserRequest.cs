using MediatR;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.UserFeature.DTOs;

namespace VehicleVault.Application.Features.UserFeature.Commands.Requests;

public record UpdateUserRequest(string email, UpdateUserDto?  UserDto): IRequest<AuthenticationResponse>;