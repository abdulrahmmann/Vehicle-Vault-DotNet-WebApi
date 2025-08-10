using MediatR;
using Microsoft.Extensions.Logging;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.ColorsFeatures.DTOs;
using VehicleVault.Application.Features.ColorsFeatures.Queries.Requests;
using VehicleVault.Domain.Entities;
using VehicleVault.Infrastructure.UOF;

namespace VehicleVault.Application.Features.ColorsFeatures.Queries.Handler;

public class GetColorByIdHandler: IRequestHandler<GetColorByIdRequest, BaseResponse<ColorDto>>
{
    #region Instance Fields
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetColorByIdHandler> _logger;
    #endregion
    
    #region Constructor
    public GetColorByIdHandler(IUnitOfWork unitOfWork, ILogger<GetColorByIdHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    #endregion
    
    
    public async Task<BaseResponse<ColorDto>> Handle(GetColorByIdRequest request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.Id <= 0)
            {
                return BaseResponse<ColorDto>.ValidationError("Id must be greater than zero");
            }

            var color = await _unitOfWork.GetRepository<Color>().GetByIdAsync(request.Id);

            if (color is null)
            {
                return BaseResponse<ColorDto>.NotFound("Color not found");
            }

            var colorMapped = new ColorDto(color.Id, color.Name, color.Code, color.FinishType, color.IsActive);
            
            return BaseResponse<ColorDto>.Success(colorMapped);
        }
        catch (Exception e)
        {
            _logger.LogError("An error occured while Retrieve Color: {error}", e.Message);
            return BaseResponse<ColorDto>.InternalError($"An error occured while Retrieve Color: {e.Message}");
        }
    }
}