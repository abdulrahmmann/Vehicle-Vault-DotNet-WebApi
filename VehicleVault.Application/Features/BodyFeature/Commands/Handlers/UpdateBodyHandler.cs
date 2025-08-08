using MediatR;
using Microsoft.Extensions.Logging;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.BodyFeature.Commands.Requests;
using VehicleVault.Infrastructure.UOF;

namespace VehicleVault.Application.Features.BodyFeature.Commands.Handlers;

public class UpdateBodyHandler: IRequestHandler<UpdateBodyRequest, BaseResponse<Unit>>
{
    #region Instance Fields
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UpdateBodyHandler> _logger;
    #endregion

    #region Constructor
    public UpdateBodyHandler(IUnitOfWork unitOfWork, ILogger<UpdateBodyHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    #endregion
    
    
    public async Task<BaseResponse<Unit>> Handle(UpdateBodyRequest request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.Id <= 0)
            {
                return BaseResponse<Unit>.ValidationError("Id must be greater than zero");
            }
            
            if (string.IsNullOrWhiteSpace(request.BodyDto.Name))
            {
                return BaseResponse<Unit>.ValidationError("MakeDto.Name cannot be empty");
            }
            
            var existingBody = await _unitOfWork.GetBodyRepository.GetByIdAsync(request.Id);
            
            if (existingBody == null)
            {
                return BaseResponse<Unit>.NotFound("Body not found");
            }
            
            existingBody.UpdateBody(request.BodyDto.Name);
            
            await _unitOfWork.SaveChangesAsync();
            
            return BaseResponse<Unit>.Success($"Body with Id: {request.Id} updated successfully");
        }
        catch (Exception e)
        {
            _logger.LogError("An error occured while Updating Body: {error}", e.Message);
            return BaseResponse<Unit>.InternalError($"An error occured while Updating Body: {e.Message}");
        }
    }
}