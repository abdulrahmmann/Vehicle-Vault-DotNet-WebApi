using MediatR;
using Microsoft.Extensions.Logging;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.FuelTypesFeature.Commands.Requests;
using VehicleVault.Domain.Entities;
using VehicleVault.Infrastructure.UOF;

namespace VehicleVault.Application.Features.FuelTypesFeature.Commands.Handler;

public class CreateFuelTypeHandler: IRequestHandler<CreateFuelTypeRequest, BaseResponse<Unit>>
{
    #region Instance Fields
    private readonly IUnitOfWork  _unitOfWork;
    private readonly ILogger<CreateFuelTypeHandler> _logger;
    #endregion

    #region Constructor
    public CreateFuelTypeHandler(IUnitOfWork unitOfWork, ILogger<CreateFuelTypeHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    #endregion

    public async Task<BaseResponse<Unit>> Handle(CreateFuelTypeRequest request, CancellationToken cancellationToken)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(request.FuelTypeDto.FuelTypeName))
            {
                return BaseResponse<Unit>.ValidationError("FuelTypeName is required");
            }

            var existingFuelType = await _unitOfWork.GetFuelTypeRepository.ExistsByNameAsync(request.FuelTypeDto.FuelTypeName);

            if (existingFuelType)
            {
                return BaseResponse<Unit>.ValidationError("FuelType with the same name already exists");
            }

            var fuelTypeMapped = new FuelType(request.FuelTypeDto.FuelTypeName);

            await _unitOfWork.GetRepository<FuelType>().AddAsync(fuelTypeMapped);
            await _unitOfWork.SaveChangesAsync();
            
            return BaseResponse<Unit>.Created($"FuelType with name: {request.FuelTypeDto.FuelTypeName} created successfully");
        }
        catch (Exception e)
        {
            _logger.LogError("An error occured while creating FuelType: {error}", e.Message);
            return BaseResponse<Unit>.InternalError($"An error occured while creating FuelType: {e.Message}");
        }
    }
}