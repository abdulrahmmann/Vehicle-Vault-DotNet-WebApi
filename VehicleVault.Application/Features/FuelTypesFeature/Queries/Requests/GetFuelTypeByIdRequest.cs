using MediatR;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.FuelTypesFeature.DTOs;

namespace VehicleVault.Application.Features.FuelTypesFeature.Queries.Requests;

public record GetFuelTypeByIdRequest(int Id): IRequest<BaseResponse<FuelTypeDto>>;