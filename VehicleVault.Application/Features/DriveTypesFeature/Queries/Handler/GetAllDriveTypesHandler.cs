using MediatR;
using Microsoft.Extensions.Logging;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.DriveTypesFeature.DTOs;
using VehicleVault.Application.Features.DriveTypesFeature.Queries.Requests;
using VehicleVault.Infrastructure.UOF;
using DriveType = VehicleVault.Domain.Entities.DriveType;

namespace VehicleVault.Application.Features.DriveTypesFeature.Queries.Handler;

public class GetAllDriveTypesHandler: IRequestHandler<GetAllDriveTypesRequest, BaseResponse<IEnumerable<DriveTypeDto>>>
{
    #region Instance Fields
    private readonly IUnitOfWork  _unitOfWork;
    private readonly ILogger<GetAllDriveTypesHandler> _logger;
    #endregion
    
    #region Constructor
    public GetAllDriveTypesHandler(IUnitOfWork unitOfWork, ILogger<GetAllDriveTypesHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    #endregion

    public async Task<BaseResponse<IEnumerable<DriveTypeDto>>> Handle(GetAllDriveTypesRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var driveTypes = await _unitOfWork.GetRepository<DriveType>().GetAllAsync();

            var enumerable = driveTypes.ToList();
            
            if (enumerable.Count == 0)
            {
                return BaseResponse<IEnumerable<DriveTypeDto>>.NoContent("No Drive Types Found");
            }
            
            var driveTypesMapped = enumerable.Select(ft => new DriveTypeDto(ft.Id, ft.Name));
            
            return BaseResponse<IEnumerable<DriveTypeDto>>.Success(driveTypesMapped, "DriveTypes Retrieved Successfully", enumerable.Count);
        }
        catch (Exception e)
        {
            _logger.LogError("An error occured while Retrieving DriveTypes: {error}", e.Message);
            return BaseResponse<IEnumerable<DriveTypeDto>>.InternalError($"An error occured while Retrieving DriveTypes: {e.Message}");
        }
    }
}