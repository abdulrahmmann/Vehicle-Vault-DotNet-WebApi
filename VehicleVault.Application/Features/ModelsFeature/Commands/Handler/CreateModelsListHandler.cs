using MediatR;
using Microsoft.Extensions.Logging;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.ModelsFeature.Commands.Requests;
using VehicleVault.Application.Features.ModelsFeature.DTOs;
using VehicleVault.Domain.Entities;
using VehicleVault.Infrastructure.UOF;

namespace VehicleVault.Application.Features.ModelsFeature.Commands.Handler; 

public class CreateModelsListHandler: IRequestHandler<CreateModelsListRequest, BaseResponse<Unit>>
{
    #region Instance Fields
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateModelsListHandler> _logger;
    #endregion

    #region Constructor
    public CreateModelsListHandler(IUnitOfWork unitOfWork, ILogger<CreateModelsListHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    #endregion


    public async Task<BaseResponse<Unit>> Handle(CreateModelsListRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var validModelsToAdd = new List<CreateModelDto>();
            var invalidModelsToRemove = new List<string>();

            foreach (var model in request.ModelsDto)
            {
                if (string.IsNullOrWhiteSpace(model.ModelName))
                {
                    _logger.LogError("Invalid Model name: {ModelName}", model.ModelName);
                    invalidModelsToRemove.Add(model.ModelName);
                    continue;
                }

                var isModelExist = await _unitOfWork.GetModelsRepository.ExistsByNameAsync(model.ModelName);

                if (isModelExist)
                {
                    _logger.LogError("Model already exists: {ModelName}", model.ModelName);
                    invalidModelsToRemove.Add(model.ModelName);
                    continue;
                }

                else
                {
                    validModelsToAdd.Add(model);
                }
            }

            var modelsMapped = validModelsToAdd.Select(m => new Model(m.ModelName, m.MakeId));
            
            if (validModelsToAdd.Count == 0)
            {
                _logger.LogWarning("No valid Models to add");
                return BaseResponse<Unit>.BadRequest("No valid Models to add");
            }

            if (invalidModelsToRemove.Count != 0)
            {
                _logger.LogWarning("some invalid Models not added: {invalidModels}", invalidModelsToRemove.ToList());
            }

            await _unitOfWork.GetRepository<Model>().AddRangeAsync(modelsMapped);
            await _unitOfWork.SaveChangesAsync();
            
            if (validModelsToAdd.Count != 0 && invalidModelsToRemove.Count != 0)
            {
                return BaseResponse<Unit>
                    .Success($"{validModelsToAdd.Count} valid Models added and {invalidModelsToRemove.Count} invalid Models");
            }
            
            return BaseResponse<Unit>.Success($"All Models were created: {validModelsToAdd.Count} / {request.ModelsDto.Count()}");
        }
        catch (Exception e)
        {
            _logger.LogError("An error occured while creating Models: {error}", e.Message);
            return BaseResponse<Unit>.InternalError($"An error occured while creating Models: {e.Message}");
        }
    }
}