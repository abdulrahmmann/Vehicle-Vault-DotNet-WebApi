using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.ColorsFeatures.DTOs;
using VehicleVault.Application.Features.ColorsFeatures.Queries.Requests;
using VehicleVault.Infrastructure.UOF;

namespace VehicleVault.Application.Features.ColorsFeatures.Queries.Handler;

public class GetColorByNameHandler: IRequestHandler<GetColorByNameRequest, BaseResponse<ColorDto>>
{
    #region Instance Fields
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetColorByNameHandler> _logger;
    #endregion
    
    #region Constructor
    public GetColorByNameHandler(IUnitOfWork unitOfWork, ILogger<GetColorByNameHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    #endregion

    public async Task<BaseResponse<ColorDto>> Handle(GetColorByNameRequest request, CancellationToken cancellationToken)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(request.ColorName))
            {
                return BaseResponse<ColorDto>.ValidationError("ColorName is required");
            }

            var color = await _unitOfWork.dbContext.Colors
                .FirstOrDefaultAsync(c => c.Name == request.ColorName, cancellationToken);

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