using MediatR;
using VehicleVault.Application.Common;

namespace VehicleVault.Application.Features.CategoryFeature.Queries.Requests;

public record SoftDeleteCategoryRequest(int Id): IRequest<BaseResponse<Unit>>;