using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.FeaturesVFeature.Commands.Requests;
using VehicleVault.Domain.Entities;
using VehicleVault.Infrastructure.UOF;

namespace VehicleVault.Application.Features.FeaturesVFeature.Commands.Handler;

public class CreateFeatureHandler: IRequestHandler<CreateFeatureRequest, BaseResponse<Unit>>
{
    #region Instance Fields
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateFeatureHandler> _logger;
    #endregion
    
    #region Constructor
    public CreateFeatureHandler(IUnitOfWork unitOfWork, ILogger<CreateFeatureHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    #endregion

    public async Task<BaseResponse<Unit>> Handle(CreateFeatureRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var requestFeature = request.FeatureDto;
            
            if (requestFeature == null) return BaseResponse<Unit>.BadRequest();

            var existingFeature = await _unitOfWork.dbContext.Features
                .FirstOrDefaultAsync(f => f.Name == requestFeature.FeatureName, cancellationToken);
            
            if (existingFeature != null) return BaseResponse<Unit>.Conflict("Feature already exists");

            var featureMapped = new Feature(requestFeature.FeatureName);

            await _unitOfWork.GetRepository<Feature>().AddAsync(featureMapped);
            await _unitOfWork.SaveChangesAsync();
            
            return BaseResponse<Unit>.Created($"Feature with name: {requestFeature.FeatureName} is successfully created");

        }
        catch (Exception e)
        {
            _logger.LogError("An error occured while creating Feature: {error}", e.Message);
            return BaseResponse<Unit>.InternalError($"An error occured while creating Feature: {e.Message}");
        }
    }
}