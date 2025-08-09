using MediatR;
using Microsoft.Extensions.Logging;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.DriveTypesFeature.Commands.Requests;
using VehicleVault.Application.Features.DriveTypesFeature.DTOs;
using VehicleVault.Infrastructure.UOF;
using DriveType = VehicleVault.Domain.Entities.DriveType;

namespace VehicleVault.Application.Features.DriveTypesFeature.Commands.Handler;

public class CreateDriveTypeListHandler: IRequestHandler<CreateDriveTypeListRequest, BaseResponse<Unit>>
{
    #region Instance Fields
    private readonly IUnitOfWork  _unitOfWork;
    private readonly ILogger<CreateDriveTypeListHandler> _logger;
    #endregion

    #region Constructor
    public CreateDriveTypeListHandler(IUnitOfWork unitOfWork, ILogger<CreateDriveTypeListHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    #endregion
    
    public async Task<BaseResponse<Unit>> Handle(CreateDriveTypeListRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var validDriveTypesToAdd = new List<CreateDriveTypeDto>();
            var invalidDriveTypesToRemove = new List<string>();

            foreach (var driveType in request.DriveTypesDto)
            {
                if (string.IsNullOrWhiteSpace(driveType.DriveTypeName))
                {
                    _logger.LogError("Invalid DriveTypeName: {makeName}", driveType.DriveTypeName);
                    invalidDriveTypesToRemove.Add(driveType.DriveTypeName);
                    continue;
                }
                var existingDriveTypeName = await _unitOfWork.GetDriveTypeRepository.ExistsByNameAsync(driveType.DriveTypeName);

                if (existingDriveTypeName)
                {
                    _logger.LogError("driveType is already exists: {driveType}", driveType.DriveTypeName);
                    invalidDriveTypesToRemove.Add(driveType.DriveTypeName);
                    continue;
                }
                else
                {
                    validDriveTypesToAdd.Add(driveType);
                }
            }
            
            var driveTypesMapped = validDriveTypesToAdd.Select(b => new DriveType(b.DriveTypeName));
            
            if (validDriveTypesToAdd.Count == 0)
            {
                _logger.LogWarning("No valid DriveType to add");
                return BaseResponse<Unit>.BadRequest("No valid DriveType to add");
            }

            if (invalidDriveTypesToRemove.Count != 0)
            {
                _logger.LogWarning("some invalid bodies not added: {invalidMakes}", invalidDriveTypesToRemove.ToList());
            }

            await _unitOfWork.GetRepository<DriveType>().AddRangeAsync(driveTypesMapped);
            await _unitOfWork.SaveChangesAsync();
            
            return BaseResponse<Unit>.Success($"All bodies were created: {validDriveTypesToAdd.Count} / {request.DriveTypesDto.Count()}");
        }
        catch (Exception e)
        {
            _logger.LogError("An error occured while creating driveTypesList: {error}", e.Message);
            return BaseResponse<Unit>.InternalError($"An error occured while creating driveTypesList: {e.Message}");
        }
    }
}