using MediatR;
using Microsoft.Extensions.Logging;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.CategoryFeature.DTOs;
using VehicleVault.Application.Features.CategoryFeature.Queries.Requests;
using VehicleVault.Domain.Entities;
using VehicleVault.Infrastructure.UOF;

namespace VehicleVault.Application.Features.CategoryFeature.Queries.Handler;

public class GetCategoryByNameHandler: IRequestHandler<GetCategoryByNameRequest, BaseResponse<CategoryDtoWithId>>
{
    #region Instance Fields
    private readonly IUnitOfWork  _unitOfWork;
    private readonly ILogger<GetCategoryByNameHandler> _logger;
    #endregion

    #region GetCategoryByNameHandler
    public GetCategoryByNameHandler(IUnitOfWork unitOfWork, ILogger<GetCategoryByNameHandler> logger) 
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    #endregion
    
    public async Task<BaseResponse<CategoryDtoWithId>> Handle(GetCategoryByNameRequest request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.CategoryName == null)
            {
                return BaseResponse<CategoryDtoWithId>.BadRequest("request.CategoryName is required");
            }
            
            var category = await _unitOfWork.GetCategoryRepository.GetCategoryByName(request.CategoryName);
            
            var categoryMapped = new CategoryDtoWithId(category.Id, category.Name, category.Description);
            
            return BaseResponse<CategoryDtoWithId>
                .Success(categoryMapped, $"Category with Name: {request.CategoryName} retrieved successfully", 1);
        }
        catch (Exception e)
        {
            _logger.LogError("An error occured while retrieving category by Id {error}", e.Message);
            return BaseResponse<CategoryDtoWithId>
                .InternalError("An error occured while retrieving category by Id {error}: {e.Message}");
        }
    }
}