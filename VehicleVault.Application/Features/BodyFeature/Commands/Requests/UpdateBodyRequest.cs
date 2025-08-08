using MediatR;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.BodyFeature.DTOs;

namespace VehicleVault.Application.Features.BodyFeature.Commands.Requests;

public record UpdateBodyRequest(int Id, UpdateBodyDto BodyDto): IRequest<BaseResponse<Unit>>;