using MediatR;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.BodyFeature.DTOs;

namespace VehicleVault.Application.Features.BodyFeature.Commands.Requests;

public record CreateBodyRequest(CreateBodyDto BodyDto): IRequest<BaseResponse<Unit>>;