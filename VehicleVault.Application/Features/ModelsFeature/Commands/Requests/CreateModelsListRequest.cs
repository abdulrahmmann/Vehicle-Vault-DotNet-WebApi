using MediatR;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.ModelsFeature.DTOs;

namespace VehicleVault.Application.Features.ModelsFeature.Commands.Requests;

public record CreateModelsListRequest(IEnumerable<CreateModelDto> ModelsDto): IRequest<BaseResponse<Unit>>;