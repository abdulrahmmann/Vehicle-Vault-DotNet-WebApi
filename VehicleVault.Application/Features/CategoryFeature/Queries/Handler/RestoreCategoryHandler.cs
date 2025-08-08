using MediatR;
using Microsoft.Extensions.Logging;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.CategoryFeature.Queries.Requests;
using VehicleVault.Domain.Entities;
using VehicleVault.Infrastructure.UOF;

namespace VehicleVault.Application.Features.CategoryFeature.Queries.Handler;

public class RestoreCategoryHandler: IRequestHandler<RestoreCategoryRequest, BaseResponse<Unit>>
{
    #region Instance Fields
    private readonly IUnitOfWork  _unitOfWork;
    private readonly ILogger<RestoreCategoryHandler> _logger;
    #endregion

    #region GetCategoryByNameHandler
    public RestoreCategoryHandler(IUnitOfWork unitOfWork, ILogger<RestoreCategoryHandler> logger) 
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    #endregion
    
    public async Task<BaseResponse<Unit>> Handle(RestoreCategoryRequest request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.Id <= 0)
            {
                return BaseResponse<Unit>.ValidationError("Id cannot be less than or equal zero");
            }

            var wasRestored  = await _unitOfWork.GetCategoryRepository.GetByIdIgnoreQueryFilterAsync(request.Id);

            if (wasRestored  == null)
            {
                return BaseResponse<Unit>.NotFound("Category not found");
            }

            if (!wasRestored .IsDeleted)
            {
                return BaseResponse<Unit>.Success($"Category with id {request.Id} is already active.");
            }

            await _unitOfWork.GetCategoryRepository.RestoreCategoryById(request.Id);

            await _unitOfWork.SaveChangesAsync();

            return BaseResponse<Unit>.Success($"Category with id {request.Id} has been restored");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while restoring category with Id {CategoryId}", request.Id);
            return BaseResponse<Unit>.InternalError($"An error occurred while restoring category by Id {request.Id}: {e.Message}");
        }
    }
}