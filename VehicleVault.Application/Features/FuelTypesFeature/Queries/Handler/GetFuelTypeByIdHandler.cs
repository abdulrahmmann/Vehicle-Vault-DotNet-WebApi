using System.Runtime.InteropServices;
using MediatR;
using Microsoft.Extensions.Logging;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.FuelTypesFeature.DTOs;
using VehicleVault.Application.Features.FuelTypesFeature.Queries.Requests;
using VehicleVault.Domain.Entities;
using VehicleVault.Infrastructure.UOF;

namespace VehicleVault.Application.Features.FuelTypesFeature.Queries.Handler;

public class GetFuelTypeByIdHandler: IRequestHandler<GetFuelTypeByIdRequest, BaseResponse<FuelTypeDto>>
{
    #region Instance Fields
    private readonly IUnitOfWork  _unitOfWork;
    private readonly ILogger<GetFuelTypeByIdHandler> _logger;
    #endregion

    #region Constructor
    public GetFuelTypeByIdHandler(IUnitOfWork unitOfWork, ILogger<GetFuelTypeByIdHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    #endregion
    
    public async Task<BaseResponse<FuelTypeDto>> Handle(GetFuelTypeByIdRequest request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.Id <= 0)
            {
                return BaseResponse<FuelTypeDto>.ValidationError("Id must be greater than zero");
            }

            var fuelType = await _unitOfWork.GetRepository<FuelType>().GetByIdAsync(request.Id);

            if (fuelType == null)
            {
                return BaseResponse<FuelTypeDto>.NotFound("FuelType not found");
            }

            var fuelTypeMapped = new FuelTypeDto(fuelType.Id, fuelType.Name);
            
            return BaseResponse<FuelTypeDto>.Success(fuelTypeMapped,$"FuelType By Id: {request.Id} Retrieved Successfully", 1);
        }
        catch (Exception e)
        {
            _logger.LogError("An error occured while Retrieving FuelType By Id: {error}", e.Message);
            return BaseResponse<FuelTypeDto>.InternalError($"An error occured while Retrieving FuelType By Id: {e.Message}");
        }
    }
}