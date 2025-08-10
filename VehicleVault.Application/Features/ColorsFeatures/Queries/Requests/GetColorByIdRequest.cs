using MediatR;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.ColorsFeatures.DTOs;

namespace VehicleVault.Application.Features.ColorsFeatures.Queries.Requests;

public record GetColorByIdRequest(int Id): IRequest<BaseResponse<ColorDto>>;