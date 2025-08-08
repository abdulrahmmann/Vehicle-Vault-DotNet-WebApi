using MediatR;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.CategoryFeature.DTOs;

namespace VehicleVault.Application.Features.CategoryFeature.Queries.Requests;

public record GetCategoryByNameRequest(string CategoryName): IRequest<BaseResponse<CategoryDtoWithId>>;