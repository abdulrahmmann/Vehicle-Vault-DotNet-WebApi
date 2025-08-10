using MediatR;
using Microsoft.Extensions.Logging;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.ColorsFeatures.DTOs;
using VehicleVault.Application.Features.ColorsFeatures.Queries.Requests;
using VehicleVault.Domain.Entities;
using VehicleVault.Infrastructure.UOF;

namespace VehicleVault.Application.Features.ColorsFeatures.Queries.Handler;

public class GetAllColorsHandler: IRequestHandler<GetAllColorsRequest, BaseResponse<IEnumerable<ColorDto>>>
{
    #region Instance Fields
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetAllColorsHandler> _logger;
    #endregion
    
    #region Constructor
    public GetAllColorsHandler(IUnitOfWork unitOfWork, ILogger<GetAllColorsHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    #endregion

    public async Task<BaseResponse<IEnumerable<ColorDto>>> Handle(GetAllColorsRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var colors = await _unitOfWork.GetRepository<Color>().GetAllAsync();

            var enumerable = colors.ToList();
            if (enumerable.Count == 0)
            {
                return BaseResponse<IEnumerable<ColorDto>>.NoContent("There are no colors");
            }

            var colorsMapped = enumerable
                .Select(c => new ColorDto(c.Id, c.Name, c.Code, c.FinishType, c.IsActive));
            
            return BaseResponse<IEnumerable<ColorDto>>
                .Success(colorsMapped, "Colors Retrieved Successfully", enumerable.Count);
        }
        catch (Exception e)
        {
            _logger.LogError("An error occured while Retrieve Colors: {error}", e.Message);
            return BaseResponse<IEnumerable<ColorDto>>.InternalError($"An error occured while Retrieve Colors: {e.Message}");
        }
    }
}