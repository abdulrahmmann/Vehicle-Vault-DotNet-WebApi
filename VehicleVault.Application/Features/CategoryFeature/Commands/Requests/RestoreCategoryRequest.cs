using MediatR;
using VehicleVault.Application.Common;

namespace VehicleVault.Application.Features.CategoryFeature.Commands.Requests;

public record RestoreCategoryRequest(int Id): IRequest<BaseResponse<Unit>>;