using MediatR;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.ColorsFeatures.DTOs;

namespace VehicleVault.Application.Features.ColorsFeatures.Queries.Requests;

public record GetColorByNameRequest(string ColorName): IRequest<BaseResponse<ColorDto>>;