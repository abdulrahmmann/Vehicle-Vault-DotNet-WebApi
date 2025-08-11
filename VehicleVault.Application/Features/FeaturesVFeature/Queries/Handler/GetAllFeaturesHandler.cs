using MediatR;
using Microsoft.Extensions.Logging;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.ColorsFeatures.DTOs;
using VehicleVault.Application.Features.FeaturesVFeature.DTOs;
using VehicleVault.Application.Features.FeaturesVFeature.Queries.Requests;
using VehicleVault.Domain.Entities;
using VehicleVault.Infrastructure.UOF;

namespace VehicleVault.Application.Features.FeaturesVFeature.Queries.Handler;

public class GetAllFeaturesHandler: IRequestHandler<GetAllFeaturesRequest, BaseResponse<IEnumerable<FeatureDto>>>
{
    #region Instance Fields
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetAllFeaturesHandler> _logger;
    #endregion
    
    #region Constructor
    public GetAllFeaturesHandler(IUnitOfWork unitOfWork, ILogger<GetAllFeaturesHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    #endregion

    
    public async Task<BaseResponse<IEnumerable<FeatureDto>>> Handle(GetAllFeaturesRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var features = await _unitOfWork.GetRepository<Feature>().GetAllAsync();

            var enumerable = features.ToList();
            if (enumerable.Count == 0)
            {
                return BaseResponse<IEnumerable<FeatureDto>>.NoContent("There are no features");
            }

            var colorsMapped = enumerable
                .Select(c => new FeatureDto(c.Id, c.Name));
            
            return BaseResponse<IEnumerable<FeatureDto>>
                .Success(colorsMapped, "features Retrieved Successfully", enumerable.Count);
        }
        catch (Exception e)
        {
            _logger.LogError("An error occured while Retrieve features: {error}", e.Message);
            return BaseResponse<IEnumerable<FeatureDto>>.InternalError($"An error occured while Retrieve features: {e.Message}");
        }
    }
}