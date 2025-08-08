using MediatR;
using Microsoft.Extensions.Logging;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.FuelTypesFeature.Commands.Requests;
using VehicleVault.Application.Features.FuelTypesFeature.DTOs;
using VehicleVault.Domain.Entities;
using VehicleVault.Infrastructure.UOF;

namespace VehicleVault.Application.Features.FuelTypesFeature.Commands.Handler;

public class CreateFuelTypesListHandler: IRequestHandler<CreateFuelTypesListRequest, BaseResponse<Unit>>
{
    #region Instance Fields
    private readonly IUnitOfWork  _unitOfWork;
    private readonly ILogger<CreateFuelTypesListHandler> _logger;
    #endregion

    #region Constructor
    public CreateFuelTypesListHandler(IUnitOfWork unitOfWork, ILogger<CreateFuelTypesListHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    #endregion
    
    public async Task<BaseResponse<Unit>> Handle(CreateFuelTypesListRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var validFuelTypesToAdd = new List<CreateFuelTypeDto>();
            var invalidFuelTypesToRemove = new List<string>();

            foreach (var fuelType in request.FuelTypesDto)
            {
                if (string.IsNullOrWhiteSpace(fuelType.FuelTypeName))
                {
                    _logger.LogError("Invalid FuelTypeName: {makeName}", fuelType.FuelTypeName);
                    invalidFuelTypesToRemove.Add(fuelType.FuelTypeName);
                    continue;
                }
                var existingFuelTypeName = await _unitOfWork.GetFuelTypeRepository.ExistsByNameAsync(fuelType.FuelTypeName);

                if (existingFuelTypeName)
                {
                    _logger.LogError("FuelType is already exists: {FuelType}", fuelType.FuelTypeName);
                    invalidFuelTypesToRemove.Add(fuelType.FuelTypeName);
                    continue;
                }
                else
                {
                    validFuelTypesToAdd.Add(fuelType);
                }
            }
            
            var fuelTypesMapped = validFuelTypesToAdd.Select(b => new FuelType(b.FuelTypeName));
            
            if (validFuelTypesToAdd.Count == 0)
            {
                _logger.LogWarning("No valid FuelType to add");
                return BaseResponse<Unit>.BadRequest("No valid FuelType to add");
            }

            if (invalidFuelTypesToRemove.Count != 0)
            {
                _logger.LogWarning("some invalid bodies not added: {invalidMakes}", invalidFuelTypesToRemove.ToList());
            }

            await _unitOfWork.GetRepository<FuelType>().AddRangeAsync(fuelTypesMapped);
            await _unitOfWork.SaveChangesAsync();
            
            return BaseResponse<Unit>.Success($"All bodies were created: {validFuelTypesToAdd.Count} / {request.FuelTypesDto.Count()}");
        }
        catch (Exception e)
        {
            _logger.LogError("An error occured while creating FuelTypes: {error}", e.Message);
            return BaseResponse<Unit>.InternalError($"An error occured while creating FuelTypes: {e.Message}");
        }
    }
}