using MediatR;
using Microsoft.Extensions.Logging;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.DriveTypesFeature.Commands.Requests;
using VehicleVault.Infrastructure.UOF;
using DriveType = VehicleVault.Domain.Entities.DriveType;

namespace VehicleVault.Application.Features.DriveTypesFeature.Commands.Handler;

public class CreateDriveTypeHandler: IRequestHandler<CreateDriveTypeRequest, BaseResponse<Unit>>
{
    #region Instance Fields
    private readonly IUnitOfWork  _unitOfWork;
    private readonly ILogger<CreateDriveTypeHandler> _logger;
    #endregion
    
    #region Constructor
    public CreateDriveTypeHandler(IUnitOfWork unitOfWork, ILogger<CreateDriveTypeHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    #endregion
    
    
    public async Task<BaseResponse<Unit>> Handle(CreateDriveTypeRequest request, CancellationToken cancellationToken)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(request.DriveTypeDto.DriveTypeName))
            {
                return BaseResponse<Unit>.ValidationError("DriveTypeName is required and can not be null or empty");
            }
            
            var driveType = await _unitOfWork.GetDriveTypeRepository.ExistsByNameAsync(request.DriveTypeDto.DriveTypeName);

            if (driveType)
            {
                return BaseResponse<Unit>.ValidationError("DriveType is already exist");
            }

            var driveTypeMapped = new DriveType(request.DriveTypeDto.DriveTypeName);

            await _unitOfWork.GetRepository<DriveType>().AddAsync(driveTypeMapped);
            
            await _unitOfWork.SaveChangesAsync();
            
            return BaseResponse<Unit>.Created($"DriveType with Name: {request.DriveTypeDto.DriveTypeName} created successfully");
        }
        catch (Exception e)
        {
            _logger.LogError("An error occured while creating DriveType: {error}", e.Message);
            return BaseResponse<Unit>.InternalError($"An error occured while creating DriveType: {e.Message}");
        }
    }
}