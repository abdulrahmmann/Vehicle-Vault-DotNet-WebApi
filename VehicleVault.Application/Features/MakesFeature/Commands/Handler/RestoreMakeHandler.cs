using MediatR;
using Microsoft.Extensions.Logging;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.MakesFeature.Commands.Requests;
using VehicleVault.Infrastructure.UOF;

namespace VehicleVault.Application.Features.MakesFeature.Commands.Handler;

public class RestoreMakeHandler: IRequestHandler<RestoreMakeRequest, BaseResponse<Unit>>
{
    #region Instance Fields
    private readonly IUnitOfWork  _unitOfWork;
    private readonly ILogger<RestoreMakeHandler> _logger;
    #endregion

    #region GetCategoryByNameHandler
    public RestoreMakeHandler(IUnitOfWork unitOfWork, ILogger<RestoreMakeHandler> logger) 
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    #endregion
    
    public async Task<BaseResponse<Unit>> Handle(RestoreMakeRequest request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.Id <= 0)
            {
                return BaseResponse<Unit>.ValidationError("Id cannot be less than or equal zero");
            }

            var wasRestored = await _unitOfWork.GetMakesRepository.GetByIdIgnoreQueryFilterAsync(request.Id);
            
            if (wasRestored  == null)
            {
                return BaseResponse<Unit>.NotFound("Make not found");
            }

            if (!wasRestored .IsDeleted)
            {
                return BaseResponse<Unit>.Success($"Make with id {request.Id} is already active.");
            }

            await _unitOfWork.GetMakesRepository.RestoreMakeById(request.Id);
            
            await _unitOfWork.SaveChangesAsync();
            
            return BaseResponse<Unit>.Success($"Make with id {request.Id} has been restored");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while restoring Make with Id {CategoryId}", request.Id);
            return BaseResponse<Unit>.InternalError($"An error occurred while restoring Make by Id {request.Id}: {e.Message}");
        }
    }
}