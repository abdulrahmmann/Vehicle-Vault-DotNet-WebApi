using MediatR;
using Microsoft.Extensions.Logging;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.DriveTypesFeature.DTOs;
using VehicleVault.Application.Features.DriveTypesFeature.Queries.Requests;
using VehicleVault.Infrastructure.UOF;

namespace VehicleVault.Application.Features.DriveTypesFeature.Queries.Handler;

public class GetDriveTypeByNameHandler: IRequestHandler<GetDriveTypeByNameRequest, BaseResponse<DriveTypeDto>>
{
    #region Instance Fields
    private readonly IUnitOfWork  _unitOfWork;
    private readonly ILogger<GetDriveTypeByNameHandler> _logger;
    #endregion
    
    #region Constructor
    public GetDriveTypeByNameHandler(IUnitOfWork unitOfWork, ILogger<GetDriveTypeByNameHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    #endregion
    

    public async Task<BaseResponse<DriveTypeDto>> Handle(GetDriveTypeByNameRequest request, CancellationToken cancellationToken)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return BaseResponse<DriveTypeDto>.ValidationError("DriveTypeName is required");
            }

            var driveType = await _unitOfWork.GetDriveTypeRepository.GetDriveTypeByName(request.Name);

            if (driveType == null)
            {
                return BaseResponse<DriveTypeDto>.NotFound("DriveType not found");
            }

            var driveTypeMapped = new DriveTypeDto(driveType.Id, driveType.Name);
            
            return BaseResponse<DriveTypeDto>.Success(driveTypeMapped,$"DriveType By Name: {request.Name} Retrieved Successfully", 1);
        }
        catch (Exception e)
        {
            _logger.LogError("An error occured while Retrieving DriveType By Name: {error}", e.Message);
            return BaseResponse<DriveTypeDto>.InternalError($"An error occured while Retrieving DriveType By Name: {e.Message}");
        }
    }
}