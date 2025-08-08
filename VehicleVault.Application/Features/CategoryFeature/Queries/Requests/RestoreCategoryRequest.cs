using MediatR;
using VehicleVault.Application.Common;

namespace VehicleVault.Application.Features.CategoryFeature.Queries.Requests;

public record RestoreCategoryRequest(int Id): IRequest<BaseResponse<Unit>>;