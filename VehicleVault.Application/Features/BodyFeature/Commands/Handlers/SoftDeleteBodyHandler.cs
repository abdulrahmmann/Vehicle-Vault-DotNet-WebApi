using MediatR;
using Microsoft.Extensions.Logging;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.BodyFeature.Commands.Requests;
using VehicleVault.Domain.Entities;
using VehicleVault.Infrastructure.UOF;

namespace VehicleVault.Application.Features.BodyFeature.Commands.Handlers;

public class SoftDeleteBodyHandler: IRequestHandler<SoftDeleteBodyRequest, BaseResponse<Unit>>
{
    #region Instance Fields
    private readonly IUnitOfWork  _unitOfWork;
    private readonly ILogger<SoftDeleteBodyHandler> _logger;
    #endregion

    #region GetCategoryByNameHandler
    public SoftDeleteBodyHandler(IUnitOfWork unitOfWork, ILogger<SoftDeleteBodyHandler> logger) 
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    #endregion
    
    public async Task<BaseResponse<Unit>> Handle(SoftDeleteBodyRequest request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.Id <= 0)
            {
                return BaseResponse<Unit>.ValidationError("Id cannot be less than or equal zero");
            }

            var body = await _unitOfWork.GetRepository<Body>().GetByIdAsync(request.Id);
            
            if (body == null)
            {
                return BaseResponse<Unit>.NotFound("Body not found");
            }
            
            body.SoftDeleteBody();

            await _unitOfWork.SaveChangesAsync();
            
            return BaseResponse<Unit>.Success($"Body with id {request.Id} has been deleted");
        }
        catch (Exception e)
        {
            _logger.LogError("An error occured while Soft Deleting Body by Id {error}", e.Message);
            return BaseResponse<Unit>
                .InternalError("An error occured while Soft Deleting Body by Id {error}: {e.Message}");
        }
    }
}