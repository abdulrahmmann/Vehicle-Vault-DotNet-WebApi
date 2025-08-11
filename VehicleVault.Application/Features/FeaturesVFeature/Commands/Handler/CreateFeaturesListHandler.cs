using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.FeaturesVFeature.Commands.Requests;
using VehicleVault.Application.Features.FeaturesVFeature.DTOs;
using VehicleVault.Domain.Entities;
using VehicleVault.Infrastructure.UOF;

namespace VehicleVault.Application.Features.FeaturesVFeature.Commands.Handler;

public class CreateFeaturesListHandler: IRequestHandler<CreateFeaturesListRequest, BaseResponse<Unit>>
{
    #region Instance Fields
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateFeaturesListHandler> _logger;
    #endregion
    
    #region Constructor
    public CreateFeaturesListHandler(IUnitOfWork unitOfWork, ILogger<CreateFeaturesListHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    #endregion


    public async Task<BaseResponse<Unit>> Handle(CreateFeaturesListRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var requestFeature = request.FeaturesDto.ToList();
            
            var validFeaturesToAdd = new List<CreateFeatureDto>();

            foreach (var feature in requestFeature)
            {
                var existingFeature = await _unitOfWork.dbContext.Features
                    .FirstOrDefaultAsync(f => f.Name == feature.FeatureName, cancellationToken);
                if (existingFeature != null)
                {
                    _logger.LogWarning("Feature with name: {feature} is already exists", feature.FeatureName);
                    continue;
                }
                else
                {
                    validFeaturesToAdd.Add(feature);
                }
            }
            
            var featuresMapped = validFeaturesToAdd
                .Select(c => new Feature(c.FeatureName));
                    
            await _unitOfWork.GetRepository<Feature>().AddRangeAsync(featuresMapped);
            await _unitOfWork.SaveChangesAsync();
                
            return BaseResponse<Unit>.Created();
        }
        catch (Exception e)
        {
            _logger.LogError("An error occured while creating Feature: {error}", e.Message);
            return BaseResponse<Unit>.InternalError($"An error occured while creating Feature: {e.Message}");
        }
    }
}