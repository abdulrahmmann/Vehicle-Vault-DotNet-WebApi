using MediatR;
using Microsoft.Extensions.Logging;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.FuelTypesFeature.DTOs;
using VehicleVault.Application.Features.FuelTypesFeature.Queries.Requests;
using VehicleVault.Domain.Entities;
using VehicleVault.Infrastructure.UOF;

namespace VehicleVault.Application.Features.FuelTypesFeature.Queries.Handler;

public class GetFuelTypeByNameHandler: IRequestHandler<GetFuelTypeByNameRequest, BaseResponse<FuelTypeDto>>
{
    #region Instance Fields
    private readonly IUnitOfWork  _unitOfWork;
    private readonly ILogger<GetFuelTypeByNameHandler> _logger;
    #endregion

    #region Constructor
    public GetFuelTypeByNameHandler(IUnitOfWork unitOfWork, ILogger<GetFuelTypeByNameHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    #endregion
    
    
    public async Task<BaseResponse<FuelTypeDto>> Handle(GetFuelTypeByNameRequest request, CancellationToken cancellationToken)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(request.FuelTypeName))
            {
                return BaseResponse<FuelTypeDto>.ValidationError("fuelTypeName is required");
            }

            var fuelType = await _unitOfWork.GetFuelTypeRepository.GetFuelTypeByName(request.FuelTypeName);

            if (fuelType == null)
            {
                return BaseResponse<FuelTypeDto>.NotFound("FuelType not found");
            }

            var fuelTypeMapped = new FuelTypeDto(fuelType.Id, fuelType.Name);
            
            return BaseResponse<FuelTypeDto>.Success(fuelTypeMapped,$"FuelType By Name: {request.FuelTypeName} Retrieved Successfully", 1);
        }
        catch (Exception e)
        {
            _logger.LogError("An error occured while Retrieving FuelType By Name: {error}", e.Message);
            return BaseResponse<FuelTypeDto>.InternalError($"An error occured while Retrieving FuelType By Name: {e.Message}");
        }
    }
}