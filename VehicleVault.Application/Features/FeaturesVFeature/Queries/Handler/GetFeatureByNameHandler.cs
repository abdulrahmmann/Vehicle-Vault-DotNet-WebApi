using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.FeaturesVFeature.DTOs;
using VehicleVault.Application.Features.FeaturesVFeature.Queries.Requests;
using VehicleVault.Domain.Entities;
using VehicleVault.Infrastructure.UOF;

namespace VehicleVault.Application.Features.FeaturesVFeature.Queries.Handler;

public class GetFeatureByNameHandler: IRequestHandler<GetFeatureByNameRequest, BaseResponse<FeatureDto>>
{
    #region Instance Fields
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetFeatureByNameHandler> _logger;
    #endregion
    
    #region Constructor
    public GetFeatureByNameHandler(IUnitOfWork unitOfWork, ILogger<GetFeatureByNameHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    #endregion

    public async Task<BaseResponse<FeatureDto>> Handle(GetFeatureByNameRequest request, CancellationToken cancellationToken)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return BaseResponse<FeatureDto>.ValidationError("Name is required");
            }

            var feature = await _unitOfWork.dbContext.Features
                .FirstOrDefaultAsync(f => f.Name == request.Name, cancellationToken);

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