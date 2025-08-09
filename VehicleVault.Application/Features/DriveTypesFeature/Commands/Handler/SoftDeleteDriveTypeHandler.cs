using MediatR;
using Microsoft.Extensions.Logging;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.DriveTypesFeature.Commands.Requests;
using VehicleVault.Infrastructure.UOF;
using DriveType = VehicleVault.Domain.Entities.DriveType;

namespace VehicleVault.Application.Features.DriveTypesFeature.Commands.Handler;

public class SoftDeleteDriveTypeHandler: IRequestHandler<SoftDeleteDriveTypeRequest, BaseResponse<Unit>>
{
    #region Instance Fields
    private readonly IUnitOfWork  _unitOfWork;
    private readonly ILogger<SoftDeleteDriveTypeHandler> _logger;
    #endregion

    #region Constructor
    public SoftDeleteDriveTypeHandler(IUnitOfWork unitOfWork, ILogger<SoftDeleteDriveTypeHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    #endregion
    
    
    public async Task<BaseResponse<Unit>> Handle(SoftDeleteDriveTypeRequest request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.Id <= 0)
            {
                return BaseResponse<Unit>.ValidationError("Id cannot be less than or equal zero");
            }

            var driveType = await _unitOfWork.GetRepository<DriveType>().GetByIdAsync(request.Id);
            
            if (driveType == null)
            {
                return BaseResponse<Unit>.NotFound("DriveType not found");
            }
            
            driveType.SoftDeleteDriveType();

            await _unitOfWork.SaveChangesAsync();
            
            return BaseResponse<Unit>.Success($"DriveType with id {request.Id} has been deleted");
        }
        catch (Exception e)
        {
            _logger.LogError("An error occured while Soft Deleting DriveType by Id {error}", e.Message);
            return BaseResponse<Unit>
                .InternalError($"An error occured while Soft Deleting DriveType by Id {e.Message}");
        }
    }
}