using MediatR;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.ColorsFeatures.DTOs;

namespace VehicleVault.Application.Features.ColorsFeatures.Commands.Requests;

public record CreateColorsListRequest(IEnumerable<CreateColorDto> ColorsDto): IRequest<BaseResponse<Unit>>;