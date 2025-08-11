using MediatR;
using Microsoft.Extensions.Logging;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.FeaturesVFeature.DTOs;
using VehicleVault.Application.Features.FeaturesVFeature.Queries.Requests;
using VehicleVault.Domain.Entities;
using VehicleVault.Infrastructure.UOF;

namespace VehicleVault.Application.Features.FeaturesVFeature.Queries.Handler;

public class GetFeatureByIdHandler: IRequestHandler<GetFeatureByIdRequest, BaseResponse<FeatureDto>>
{
    #region Instance Fields
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetFeatureByIdHandler> _logger;
    #endregion
    
    #region Constructor
    public GetFeatureByIdHandler(IUnitOfWork unitOfWork, ILogger<GetFeatureByIdHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    #endregion


    public async Task<BaseResponse<FeatureDto>> Handle(GetFeatureByIdRequest request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.Id < 0)
            {
                return BaseResponse<FeatureDto>.ValidationError("Invalid feature id");
            }

            var feature = await _unitOfWork.GetRepository<Feature>().GetByIdAsync(request.Id);

            if (feature == null)
            {
                return BaseResponse<FeatureDto>.NotFound("Feature not found");
            }

            var featureMapped = new FeatureDto(feature.Id, feature.Name);
            
            return BaseResponse<FeatureDto>.Success(featureMapped);
        }
        catch (Exception e)
        {
            _logger.LogError("An error occured while Retrieve features: {error}", e.Message);
            return BaseResponse<FeatureDto>.InternalError($"An error occured while Retrieve features: {e.Message}");
        }
    }
}