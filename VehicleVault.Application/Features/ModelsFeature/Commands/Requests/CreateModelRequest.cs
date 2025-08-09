using MediatR;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.ModelsFeature.DTOs;

namespace VehicleVault.Application.Features.ModelsFeature.Commands.Requests;

public record CreateModelRequest(CreateModelDto ModelDto): IRequest<BaseResponse<Unit>>;