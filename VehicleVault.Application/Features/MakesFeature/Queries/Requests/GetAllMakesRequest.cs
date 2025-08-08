using MediatR;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.MakesFeature.DTOs;

namespace VehicleVault.Application.Features.MakesFeature.Queries.Requests;

public record GetAllMakesRequest(): IRequest<BaseResponse<IEnumerable<MakeDto>>>;