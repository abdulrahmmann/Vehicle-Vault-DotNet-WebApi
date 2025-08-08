using MediatR;
using Microsoft.Extensions.Logging;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.FuelTypesFeature.DTOs;
using VehicleVault.Application.Features.FuelTypesFeature.Queries.Requests;
using VehicleVault.Domain.Entities;
using VehicleVault.Infrastructure.UOF;

namespace VehicleVault.Application.Features.FuelTypesFeature.Queries.Handler;

public class GetAllFuelTypesHandler: IRequestHandler<GetAllFuelTypesRequest, BaseResponse<IEnumerable<FuelTypeDto>>>
{
    #region Instance Fields
    private readonly IUnitOfWork  _unitOfWork;
    private readonly ILogger<GetAllFuelTypesHandler> _logger;
    #endregion

    
    #region Constructor
    public GetAllFuelTypesHandler(IUnitOfWork unitOfWork, ILogger<GetAllFuelTypesHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    #endregion

    public async Task<BaseResponse<IEnumerable<FuelTypeDto>>> Handle(GetAllFuelTypesRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var fuelTypes = await _unitOfWork.GetRepository<FuelType>().GetAllAsync();

            var enumerable = fuelTypes.ToList();
            
            if (enumerable.Count == 0)
            {
                return BaseResponse<IEnumerable<FuelTypeDto>>.NoContent("No Fuel Types Found");
            }
            
            var fuelTypesMapped = enumerable.Select(ft => new FuelTypeDto(ft.Id, ft.Name));
            
            return BaseResponse<IEnumerable<FuelTypeDto>>.Success(fuelTypesMapped, "FuelTypes Retrieved Successfully", enumerable.Count);
        }
        catch (Exception e)
        {
            _logger.LogError("An error occured while Retrieving FuelTypes: {error}", e.Message);
            return BaseResponse<IEnumerable<FuelTypeDto>>.InternalError($"An error occured while Retrieving FuelTypes: {e.Message}");
        }
    }
}