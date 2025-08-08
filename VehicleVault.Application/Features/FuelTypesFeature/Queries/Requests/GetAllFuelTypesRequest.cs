using MediatR;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.FuelTypesFeature.DTOs;

namespace VehicleVault.Application.Features.FuelTypesFeature.Queries.Requests;

public record GetAllFuelTypesRequest(): IRequest<BaseResponse<IEnumerable<FuelTypeDto>>>;