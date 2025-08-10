using MediatR;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.ColorsFeatures.DTOs;

namespace VehicleVault.Application.Features.ColorsFeatures.Commands.Requests;

public record CreateColorRequest(CreateColorDto ColorDto): IRequest<BaseResponse<Unit>>;