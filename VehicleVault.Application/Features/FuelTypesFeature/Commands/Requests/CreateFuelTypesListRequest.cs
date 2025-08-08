using MediatR;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.FuelTypesFeature.DTOs;

namespace VehicleVault.Application.Features.FuelTypesFeature.Commands.Requests;

public record CreateFuelTypesListRequest(IEnumerable<CreateFuelTypeDto> FuelTypesDto): IRequest<BaseResponse<Unit>>;