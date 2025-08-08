using MediatR;
using Microsoft.Extensions.Logging;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.BodyFeature.Commands.Requests;
using VehicleVault.Domain.Entities;
using VehicleVault.Infrastructure.UOF;

namespace VehicleVault.Application.Features.BodyFeature.Commands.Handlers;

public class CreateBodyHandler: IRequestHandler<CreateBodyRequest, BaseResponse<Unit>>
{
    #region Instance Fields
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateBodyHandler> _logger;
    #endregion

    #region Constructor
    public CreateBodyHandler(IUnitOfWork unitOfWork, ILogger<CreateBodyHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    #endregion
    
    
    public async Task<BaseResponse<Unit>> Handle(CreateBodyRequest request, CancellationToken cancellationToken)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(request.BodyDto.Name))
            {
                return BaseResponse<Unit>.ValidationError("request body name is required");
            }
            
            var existingBody = await _unitOfWork.GetBodyRepository.ExistsByNameAsync(request.BodyDto.Name);
            
            if (existingBody)
            {
                return BaseResponse<Unit>.Conflict($"Body with name: {request.BodyDto.Name} already exists");
            }

            var bodyMapped = new Body(request.BodyDto.Name);

            await _unitOfWork.GetRepository<Body>().AddAsync(bodyMapped);
            
            await _unitOfWork.SaveChangesAsync();
            
            return BaseResponse<Unit>.Created($"Body with name: {request.BodyDto.Name} is successfully created");
        }
        catch (Exception e)
        {
            _logger.LogError("An error occured while creating Body: {error}", e.Message);
            return BaseResponse<Unit>.InternalError($"An error occured while creating Body: {e.Message}");
        }
    }
}