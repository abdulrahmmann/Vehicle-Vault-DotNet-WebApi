using MediatR;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.MakesFeature.DTOs;

namespace VehicleVault.Application.Features.MakesFeature.Commands.Requests;

public record UpdateMakeRequest(int Id, UpdateMakeDto MakeDto): IRequest<BaseResponse<Unit>>;