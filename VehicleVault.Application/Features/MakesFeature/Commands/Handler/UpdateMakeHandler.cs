using MediatR;
using Microsoft.Extensions.Logging;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.MakesFeature.Commands.Requests;
using VehicleVault.Infrastructure.UOF;

namespace VehicleVault.Application.Features.MakesFeature.Commands.Handler;

public class UpdateMakeHandler: IRequestHandler<UpdateMakeRequest, BaseResponse<Unit>>
{
    #region Instance Fields
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UpdateMakeHandler> _logger;
    #endregion

    #region Constructor
    public UpdateMakeHandler(IUnitOfWork unitOfWork, ILogger<UpdateMakeHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    #endregion
    
    public async Task<BaseResponse<Unit>> Handle(UpdateMakeRequest request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.Id <= 0)
            {
                return BaseResponse<Unit>.ValidationError("Id must be greater than zero");
            }

            if (string.IsNullOrWhiteSpace(request.MakeDto.Name))
            {
                return BaseResponse<Unit>.ValidationError("MakeDto.Name cannot be empty");
            }

            var existingMake = await _unitOfWork.GetMakesRepository.GetByIdAsync(request.Id);

            if (existingMake == null)
            {
                return BaseResponse<Unit>.NotFound("Make not found");
            }
            
            existingMake.UpdateMake(request.MakeDto.Name);
            
            await _unitOfWork.SaveChangesAsync();
            
            return BaseResponse<Unit>.Success($"Make with Id: {request.Id} updated successfully");
        }
        catch (Exception e)
        {
            _logger.LogError("An error occured while Updating Make: {error}", e.Message);
            return BaseResponse<Unit>.InternalError($"An error occured while Updating Make: {e.Message}");
        }
    }
}