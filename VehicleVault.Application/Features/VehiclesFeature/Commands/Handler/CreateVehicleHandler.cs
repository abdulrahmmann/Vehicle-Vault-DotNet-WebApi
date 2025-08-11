using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.VehiclesFeature.Commands.Requests;
using VehicleVault.Application.Features.VehiclesFeature.DTOs;
using VehicleVault.Infrastructure.UOF;

namespace VehicleVault.Application.Features.VehiclesFeature.Commands.Handler;

public class CreateVehicleHandler: IRequestHandler<CreateVehicleRequest, BaseResponse<Unit>>
{
    #region Instance Fields
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreateVehicleDto>  _validator;
    private readonly ILogger<CreateVehicleHandler> _logger;
    #endregion

    #region Constructor
    public CreateVehicleHandler(IUnitOfWork unitOfWork, ILogger<CreateVehicleHandler> logger, IValidator<CreateVehicleDto> validator)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _validator = validator;
    }
    #endregion
    

    public async Task<BaseResponse<Unit>> Handle(CreateVehicleRequest request, CancellationToken cancellationToken)
    {
        try
        {
            throw new NotImplementedException();
        }
        catch (Exception e)
        {
            _logger.LogError("An error occured while creating Vehicle: {error}", e.Message);
            return BaseResponse<Unit>.InternalError($"An error occured while creating Vehicle: {e.Message}");
        }
    }
}