using MediatR;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.VehiclesFeature.DTOs;

namespace VehicleVault.Application.Features.VehiclesFeature.Queries.Requests;

public record GetAllVehiclesRequest(): IRequest<BaseResponse<IEnumerable<VehicleSummaryDto>>>;