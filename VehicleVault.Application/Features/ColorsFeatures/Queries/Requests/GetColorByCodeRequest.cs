using MediatR;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.ColorsFeatures.DTOs;

namespace VehicleVault.Application.Features.ColorsFeatures.Queries.Requests;

public record GetColorByCodeRequest(string ColorCode): IRequest<BaseResponse<ColorDto>>;