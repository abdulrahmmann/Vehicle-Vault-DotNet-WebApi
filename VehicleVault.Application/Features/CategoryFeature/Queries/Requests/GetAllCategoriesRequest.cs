using MediatR;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.CategoryFeature.DTOs;

namespace VehicleVault.Application.Features.CategoryFeature.Queries.Requests;

public record GetAllCategoriesRequest(): IRequest<BaseResponse<IEnumerable<CategoryDtoWithId>>>;