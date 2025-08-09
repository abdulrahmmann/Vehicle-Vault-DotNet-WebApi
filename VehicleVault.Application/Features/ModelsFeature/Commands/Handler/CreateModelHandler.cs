using MediatR;
using Microsoft.Extensions.Logging;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.ModelsFeature.Commands.Requests;
using VehicleVault.Domain.Entities;
using VehicleVault.Infrastructure.UOF;

namespace VehicleVault.Application.Features.ModelsFeature.Commands.Handler;

public class CreateModelHandler: IRequestHandler<CreateModelRequest, BaseResponse<Unit>>
{
    #region Instance Fields
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateModelHandler> _logger;
    #endregion

    #region Constructor
    public CreateModelHandler(IUnitOfWork unitOfWork, ILogger<CreateModelHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    #endregion

    public async Task<BaseResponse<Unit>> Handle(CreateModelRequest request, CancellationToken cancellationToken)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(request.ModelDto.ModelName))
            {
                return BaseResponse<Unit>.ValidationError("ModelName is required");
            }

            var existingModel = await _unitOfWork.GetModelsRepository.ExistsByNameAsync(request.ModelDto.ModelName);

            if (existingModel)
            {
                return BaseResponse<Unit>.ValidationError("Model with the same name already exists");
            }
            
            var modelMapped = new Model(request.ModelDto.ModelName);

            await _unitOfWork.GetRepository<Model>().AddAsync(modelMapped);
            await _unitOfWork.SaveChangesAsync();
            
            return BaseResponse<Unit>.Created($"Model with name: {request.ModelDto.ModelName} created successfully");
        }
        catch (Exception e)
        {
            _logger.LogError("An error occured while creating Model: {error}", e.Message);
            return BaseResponse<Unit>.InternalError($"An error occured while creating Model: {e.Message}");
        }
    }
}