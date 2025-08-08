using MediatR;
using Microsoft.Extensions.Logging;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.MakesFeature.Commands.Requests;
using VehicleVault.Domain.Entities;
using VehicleVault.Infrastructure.UOF;

namespace VehicleVault.Application.Features.MakesFeature.Commands.Handler;

public class SoftDeleteMakeHandler: IRequestHandler<SoftDeleteMakeRequest, BaseResponse<Unit>>
{
    #region Instance Fields
    private readonly IUnitOfWork  _unitOfWork;
    private readonly ILogger<SoftDeleteMakeHandler> _logger;
    #endregion

    #region GetCategoryByNameHandler
    public SoftDeleteMakeHandler(IUnitOfWork unitOfWork, ILogger<SoftDeleteMakeHandler> logger) 
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    #endregion
    
    public async Task<BaseResponse<Unit>> Handle(SoftDeleteMakeRequest request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.Id <= 0)
            {
                return BaseResponse<Unit>.ValidationError("Id cannot be less than or equal zero");
            }

            var make = await _unitOfWork.GetRepository<Make>().GetByIdAsync(request.Id);
            
            if (make == null)
            {
                return BaseResponse<Unit>.NotFound("Make not found");
            }
            
            make.SoftDeleteMake();

            await _unitOfWork.SaveChangesAsync();
            
            return BaseResponse<Unit>.Success($"Make with id {request.Id} has been deleted");
        }
        catch (Exception e)
        {
            _logger.LogError("An error occured while Soft Deleting Make by Id {error}", e.Message);
            return BaseResponse<Unit>
                .InternalError("An error occured while Soft Deleting Make by Id {error}: {e.Message}");
        }
    }
}