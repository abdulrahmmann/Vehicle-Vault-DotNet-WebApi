using MediatR;
using Microsoft.Extensions.Logging;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.ColorsFeatures.Commands.Requests;
using VehicleVault.Application.Features.ColorsFeatures.DTOs;
using VehicleVault.Domain.Entities;
using VehicleVault.Infrastructure.UOF;

namespace VehicleVault.Application.Features.ColorsFeatures.Commands.Handler;

public class CreateColorsListHandler: IRequestHandler<CreateColorsListRequest, BaseResponse<Unit>>
{
    #region Instance Fields
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateColorsListHandler> _logger;
    #endregion
    
    #region Constructor
    public CreateColorsListHandler(IUnitOfWork unitOfWork, ILogger<CreateColorsListHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    #endregion

    public async Task<BaseResponse<Unit>> Handle(CreateColorsListRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var requestColor = request.ColorsDto.ToList();
            
            var validColorsToAdd = new List<CreateColorDto>();

            foreach (var color in request.ColorsDto)
            {
                var existingColor = _unitOfWork.dbContext.Colors.FirstOrDefault(
                    c => c.Name == color.Name && 
                    c.Code == color.Code && 
                    c.FinishType == color.FinishType && 
                    c.IsActive == color.IsActive);
                if (existingColor != null)
                {
                    _logger.LogWarning("Color with name: {} is already exists", existingColor!.Name);
                    continue;
                }
                else
                {
                    validColorsToAdd.Add(color);
                }
            }
            
            var colorsMapped = validColorsToAdd
                .Select(c => new Color(c.Name, c.Code, c.FinishType, c.IsActive));
                    
            await _unitOfWork.GetRepository<Color>().AddRangeAsync(colorsMapped);
            await _unitOfWork.SaveChangesAsync();
                
            return BaseResponse<Unit>.Created();
        }
        catch (Exception e)
        {
            _logger.LogError("An error occured while creating category: {error}", e.Message);
            return BaseResponse<Unit>.InternalError($"An error occured while creating category: {e.Message}");
        }
    }
}