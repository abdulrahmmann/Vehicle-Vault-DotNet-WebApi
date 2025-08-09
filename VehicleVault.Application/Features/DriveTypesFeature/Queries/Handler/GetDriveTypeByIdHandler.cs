using MediatR;
using Microsoft.Extensions.Logging;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.DriveTypesFeature.DTOs;
using VehicleVault.Application.Features.DriveTypesFeature.Queries.Requests;
using VehicleVault.Infrastructure.UOF;
using DriveType = VehicleVault.Domain.Entities.DriveType;

namespace VehicleVault.Application.Features.DriveTypesFeature.Queries.Handler;

public class GetDriveTypeByIdHandler: IRequestHandler<GetDriveTypeByIdRequest, BaseResponse<DriveTypeDto>>
{
    #region Instance Fields
    private readonly IUnitOfWork  _unitOfWork;
    private readonly ILogger<GetDriveTypeByIdHandler> _logger;
    #endregion
    
    #region Constructor
    public GetDriveTypeByIdHandler(IUnitOfWork unitOfWork, ILogger<GetDriveTypeByIdHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    #endregion
    
    public async Task<BaseResponse<DriveTypeDto>> Handle(GetDriveTypeByIdRequest request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.Id <= 0)
            {
                return BaseResponse<DriveTypeDto>.ValidationError("Id must be greater than zero");
            }

            var driveType = await _unitOfWork.GetRepository<DriveType>().GetByIdAsync(request.Id);

            if (driveType == null)
            {
                return BaseResponse<DriveTypeDto>.NotFound("driveType not found");
            }
            
            var driveTypeMapped = new DriveTypeDto(driveType.Id, driveType.Name);
            
            return BaseResponse<DriveTypeDto>.Success(driveTypeMapped,$"DriveType By Id: {request.Id} Retrieved Successfully", 1);
        }
        catch (Exception e)
        {
            _logger.LogError("An error occured while Retrieving DriveType By Id: {error}", e.Message);
            return BaseResponse<DriveTypeDto>.InternalError($"An error occured while Retrieving DriveType By Id: {e.Message}");
        }
    }
}