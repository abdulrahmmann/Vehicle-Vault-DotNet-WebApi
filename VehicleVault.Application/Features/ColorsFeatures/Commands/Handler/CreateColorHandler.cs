using MediatR;
using Microsoft.Extensions.Logging;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.ColorsFeatures.Commands.Requests;
using VehicleVault.Domain.Entities;
using VehicleVault.Infrastructure.UOF;

namespace VehicleVault.Application.Features.ColorsFeatures.Commands.Handler;

public class CreateColorHandler: IRequestHandler<CreateColorRequest, BaseResponse<Unit>>
{
    #region Instance Fields
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateColorHandler> _logger;
    #endregion
    
    #region Constructor
    public CreateColorHandler(IUnitOfWork unitOfWork, ILogger<CreateColorHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    #endregion
    
    
    public async Task<BaseResponse<Unit>> Handle(CreateColorRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var requestColor = request.ColorDto;
            
            if (requestColor == null) return BaseResponse<Unit>.BadRequest();

            var existingColor = _unitOfWork.dbContext.Colors.Where(c => c.Name == requestColor.Name && 
                                                                        c.Code == requestColor.Code &&
                                                                        c.FinishType == requestColor.FinishType &&
                                                                        c.IsActive == requestColor.IsActive);
            
            if (existingColor != null) return BaseResponse<Unit>.Conflict("color already exists");

            var colorMapped = new Color(requestColor.Name, requestColor.Code, requestColor.FinishType, requestColor.IsActive);

            await _unitOfWork.GetRepository<Color>().AddAsync(colorMapped);
            await _unitOfWork.SaveChangesAsync();
            
            return BaseResponse<Unit>.Created($"Color with name: {requestColor.Name} is successfully created");

        }
        catch (Exception e)
        {
            _logger.LogError("An error occured while creating category: {error}", e.Message);
            return BaseResponse<Unit>.InternalError($"An error occured while creating category: {e.Message}");
        }
    }
}