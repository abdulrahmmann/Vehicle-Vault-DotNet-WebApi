using MediatR;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.FuelTypesFeature.DTOs;

namespace VehicleVault.Application.Features.FuelTypesFeature.Commands.Requests;

public record CreateFuelTypeRequest(CreateFuelTypeDto FuelTypeDto): IRequest<BaseResponse<Unit>>;