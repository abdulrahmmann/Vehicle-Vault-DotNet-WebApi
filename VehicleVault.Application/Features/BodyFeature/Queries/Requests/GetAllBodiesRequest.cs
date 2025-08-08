using MediatR;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.BodyFeature.DTOs;

namespace VehicleVault.Application.Features.BodyFeature.Queries.Requests;

public record GetAllBodiesRequest(): IRequest<BaseResponse<IEnumerable<BodyDto>>>;