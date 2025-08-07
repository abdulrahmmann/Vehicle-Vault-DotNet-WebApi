using MediatR;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.CategoryFeature.DTOs;

namespace VehicleVault.Application.Features.CategoryFeature.Commands.Requests;

public record CreateCategoryRequest(CategoryDto  CategoryDto): IRequest<BaseResponse<Unit>>;