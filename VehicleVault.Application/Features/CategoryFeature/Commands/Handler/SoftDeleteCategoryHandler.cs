using MediatR;
using Microsoft.Extensions.Logging;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.CategoryFeature.Commands.Requests;
using VehicleVault.Domain.Entities;
using VehicleVault.Infrastructure.UOF;

namespace VehicleVault.Application.Features.CategoryFeature.Commands.Handler;

public class SoftDeleteCategoryHandler: IRequestHandler<SoftDeleteCategoryRequest, BaseResponse<Unit>>
{
    #region Instance Fields
    private readonly IUnitOfWork  _unitOfWork;
    private readonly ILogger<SoftDeleteCategoryHandler> _logger;
    #endregion

    #region GetCategoryByNameHandler
    public SoftDeleteCategoryHandler(IUnitOfWork unitOfWork, ILogger<SoftDeleteCategoryHandler> logger) 
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    #endregion

    public async Task<BaseResponse<Unit>> Handle(SoftDeleteCategoryRequest request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.Id <= 0)
            {
                return BaseResponse<Unit>.ValidationError("Id cannot be less than or equal zero");
            }

            var category = await _unitOfWork.GetRepository<Category>().GetByIdAsync(request.Id);

            if (category == null)
            {
                return BaseResponse<Unit>.NotFound("Category not found");
            }
            
            category.SoftDeleteCategory();

            await _unitOfWork.SaveChangesAsync();
            
            return BaseResponse<Unit>.Success($"category with id {request.Id} has been deleted");
        }
        catch (Exception e)
        {
            _logger.LogError("An error occured while Soft Deleting category by Id {error}", e.Message);
            return BaseResponse<Unit>
                .InternalError("An error occured while Soft Deleting category by Id {error}: {e.Message}");
        }
    }
}