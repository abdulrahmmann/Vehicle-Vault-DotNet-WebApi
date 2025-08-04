using MediatR;
using VehicleVault.Application.Common;
using VehicleVault.Application.Models;

namespace VehicleVault.Application.Features.UserFeature.Commands.Requests;

public record GenerateNewAccessTokenRequest(TokenModel TokenModel): IRequest<AuthenticationResponse>;