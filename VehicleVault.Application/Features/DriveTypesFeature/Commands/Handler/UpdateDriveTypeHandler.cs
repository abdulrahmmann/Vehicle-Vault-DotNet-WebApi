using MediatR;
using Microsoft.Extensions.Logging;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.DriveTypesFeature.Commands.Requests;
using VehicleVault.Infrastructure.UOF;

namespace VehicleVault.Application.Features.DriveTypesFeature.Commands.Handler;

public class UpdateDriveTypeHandler: IRequestHandler<UpdateDriveTypeRequest, BaseResponse<Unit>>
{
    #region Instance Fields
    private readonly IUnitOfWork  _unitOfWork;
    private readonly ILogger<UpdateDriveTypeHandler> _logger;
    #endregion

    #region Constructor
    public UpdateDriveTypeHandler(IUnitOfWork unitOfWork, ILogger<UpdateDriveTypeHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    #endregion
    
    
    public async Task<BaseResponse<Unit>> Handle(UpdateDriveTypeRequest request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.Id <= 0)
            {
                return BaseResponse<Unit>.ValidationError("Id must be greater than zero");
            }
            
            if (string.IsNullOrWhiteSpace(request.DriveTypeDto.DriveTypeName))
            {
                return BaseResponse<Unit>.ValidationError("DriveTypeName cannot be empty");
            }
            
            var existingDriveType = await _unitOfWork.GetDriveTypeRepository.GetByIdAsync(request.Id);
            
            if (existingDriveType == null)
            {
                return BaseResponse<Unit>.NotFound("Body not found");
            }
            
            existingDriveType.UpdateDriveType(request.DriveTypeDto.DriveTypeName);
            
            await _unitOfWork.SaveChangesAsync();
            
            return BaseResponse<Unit>.Success($"DriveType with Id: {request.Id} updated successfully");
        }
        catch (Exception e)
        {
            _logger.LogError("An error occured while Updating DriveType: {error}", e.Message);
            return BaseResponse<Unit>.InternalError($"An error occured while Updating DriveType: {e.Message}");
        }
    }
}