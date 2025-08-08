using MediatR;
using Microsoft.Extensions.Logging;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.BodyFeature.Commands.Requests;
using VehicleVault.Infrastructure.UOF;

namespace VehicleVault.Application.Features.BodyFeature.Commands.Handlers;

public class RestoreBodyHandler: IRequestHandler<RestoreBodyRequest, BaseResponse<Unit>>
{
    #region Instance Fields
    private readonly IUnitOfWork  _unitOfWork;
    private readonly ILogger<RestoreBodyHandler> _logger;
    #endregion

    #region GetCategoryByNameHandler
    public RestoreBodyHandler(IUnitOfWork unitOfWork, ILogger<RestoreBodyHandler> logger) 
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    #endregion
    
    
    public async Task<BaseResponse<Unit>> Handle(RestoreBodyRequest request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.Id <= 0)
            {
                return BaseResponse<Unit>.ValidationError("Id cannot be less than or equal zero");
            }

            var wasRestored = await _unitOfWork.GetBodyRepository.GetByIdIgnoreQueryFilterAsync(request.Id);
            
            if (wasRestored  == null)
            {
                return BaseResponse<Unit>.NotFound("Body not found");
            }

            if (!wasRestored .IsDeleted)
            {
                return BaseResponse<Unit>.Success($"Body with id {request.Id} is already active.");
            }

            await _unitOfWork.GetBodyRepository.RestoreBodyById(request.Id);
            
            await _unitOfWork.SaveChangesAsync();
            
            return BaseResponse<Unit>.Success($"Body with id {request.Id} has been restored");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while restoring Body with Id {CategoryId}", request.Id);
            return BaseResponse<Unit>.InternalError($"An error occurred while restoring Body by Id {request.Id}: {e.Message}");
        }
    }
}