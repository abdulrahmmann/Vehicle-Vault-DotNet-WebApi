using MediatR;
using Microsoft.Extensions.Logging;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.CategoryFeature.DTOs;
using VehicleVault.Application.Features.CategoryFeature.Queries.Requests;
using VehicleVault.Domain.Entities;
using VehicleVault.Infrastructure.UOF;

namespace VehicleVault.Application.Features.CategoryFeature.Queries.Handler;

public class GetCategoryByIdHandler: IRequestHandler<GetCategoryByIdRequest, BaseResponse<CategoryDtoWithId>>
{
    #region Instance Fields
    private readonly IUnitOfWork  _unitOfWork;
    private readonly ILogger<GetCategoryByIdHandler> _logger;
    #endregion

    #region Constructor
    public GetCategoryByIdHandler(IUnitOfWork unitOfWork, ILogger<GetCategoryByIdHandler> logger) 
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    #endregion
    
    public async Task<BaseResponse<CategoryDtoWithId>> Handle(GetCategoryByIdRequest request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.CategoryId <= 0)
            {
                return BaseResponse<CategoryDtoWithId>.ValidationError("CategoryId must be greater than zero");
            }

            var category = await _unitOfWork.GetRepository<Category>().GetByIdAsync(request.CategoryId);
            
            if (category == null)
            {
                return BaseResponse<CategoryDtoWithId>.NotFound("Category not found");
            }

            var categoryMapped = new CategoryDtoWithId(category.Id, category.Name, category.Description);
            
            return BaseResponse<CategoryDtoWithId>
                .Success(categoryMapped, $"Category with Id: {request.CategoryId} retrieved successfully", 1);
        }
        catch (Exception e)
        {
            _logger.LogError("An error occured while retrieving category by Id {error}", e.Message);
            return BaseResponse<CategoryDtoWithId>
                .InternalError("An error occured while retrieving category by Id {error}: {e.Message}");
        }
    }
}