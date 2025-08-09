using MediatR;
using Microsoft.Extensions.Logging;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.DriveTypesFeature.Commands.Requests;
using VehicleVault.Infrastructure.UOF;

namespace VehicleVault.Application.Features.DriveTypesFeature.Commands.Handler;

public class RestoreDriveTypeHandler: IRequestHandler<RestoreDriveTypeRequest, BaseResponse<Unit>>
{
    #region Instance Fields
    private readonly IUnitOfWork  _unitOfWork;
    private readonly ILogger<RestoreDriveTypeHandler> _logger;
    #endregion

    #region Constructor
    public RestoreDriveTypeHandler(IUnitOfWork unitOfWork, ILogger<RestoreDriveTypeHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    #endregion
    
    
    public async Task<BaseResponse<Unit>> Handle(RestoreDriveTypeRequest request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.Id <= 0)
            {
                return BaseResponse<Unit>.ValidationError("Id cannot be less than or equal zero");
            }

            var wasRestored = await _unitOfWork.GetDriveTypeRepository.GetByIdIgnoreQueryFilterAsync(request.Id);
            
            if (wasRestored  == null)
            {
                return BaseResponse<Unit>.NotFound("DriveType not found");
            }

            if (!wasRestored .IsDeleted)
            {
                return BaseResponse<Unit>.Success($"DriveType with id {request.Id} is already active.");
            }

            await _unitOfWork.GetDriveTypeRepository.RestoreDriveTypeById(request.Id);
            
            await _unitOfWork.SaveChangesAsync();
            
            return BaseResponse<Unit>.Success($"DriveType with id {request.Id} has been restored");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while restoring DriveType with Id {CategoryId}", request.Id);
            return BaseResponse<Unit>.InternalError($"An error occurred while restoring DriveType by Id {request.Id}: {e.Message}");
        }
    }
}