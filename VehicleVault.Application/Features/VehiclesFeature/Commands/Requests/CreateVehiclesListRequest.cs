using MediatR;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.VehiclesFeature.DTOs;

namespace VehicleVault.Application.Features.VehiclesFeature.Commands.Requests;

public record CreateVehiclesListRequest (IEnumerable<CreateVehicleDto> VehicleDto): IRequest<BaseResponse<Unit>>;