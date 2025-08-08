using MediatR;
using Microsoft.Extensions.Logging;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.CategoryFeature.DTOs;
using VehicleVault.Application.Features.CategoryFeature.Queries.Requests;
using VehicleVault.Domain.Entities;
using VehicleVault.Infrastructure.UOF;

namespace VehicleVault.Application.Features.CategoryFeature.Queries.Handler;

public class GetAllCategoriesHandler: IRequestHandler<GetAllCategoriesRequest, BaseResponse<IEnumerable<CategoryDtoWithId>>>
{
    #region Instance Fields
    private readonly IUnitOfWork  _unitOfWork;
    private readonly ILogger<GetAllCategoriesHandler> _logger;
    #endregion

    #region Constructor
    public GetAllCategoriesHandler(IUnitOfWork unitOfWork, ILogger<GetAllCategoriesHandler> logger) 
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    #endregion
    
    public async Task<BaseResponse<IEnumerable<CategoryDtoWithId>>> Handle(GetAllCategoriesRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var categories = await _unitOfWork.GetRepository<Category>().GetAllAsync();

            var enumerable = categories.ToList();
            
            if (enumerable.Count <= 0)
            {
                _logger.LogInformation("category entity is empty");
                return BaseResponse<IEnumerable<CategoryDtoWithId>>.NoContent("category entity is empty");
            }
            
            var categoriesMapped = 
                enumerable.Select(c => new CategoryDtoWithId(c.Id, c.Name, c.Description));
            
            return BaseResponse<IEnumerable<CategoryDtoWithId>>
                .Success(categoriesMapped, "Categories Retrieving Successfully", enumerable.Count);
        }
        catch (Exception e)
        {
            _logger.LogError("An error occured while retrieving categories: {error}", e.Message);
            return BaseResponse<IEnumerable<CategoryDtoWithId>>
                .InternalError($"An error occured while retrieving categories: {e.Message}");
        }
    }
}