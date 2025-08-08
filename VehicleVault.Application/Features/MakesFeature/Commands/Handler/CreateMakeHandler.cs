using MediatR;
using Microsoft.Extensions.Logging;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.MakesFeature.Commands.Requests;
using VehicleVault.Domain.Entities;
using VehicleVault.Infrastructure.UOF;

namespace VehicleVault.Application.Features.MakesFeature.Commands.Handler;

public class CreateMakeHandler: IRequestHandler<CreateMakeRequest, BaseResponse<Unit>>
{
    #region Instance Fields
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateMakeHandler> _logger;
    #endregion

    #region Constructor
    public CreateMakeHandler(IUnitOfWork unitOfWork, ILogger<CreateMakeHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    #endregion
    
    
    public async Task<BaseResponse<Unit>> Handle(CreateMakeRequest request, CancellationToken cancellationToken)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(request.MakeDto.Name))
            {
                return BaseResponse<Unit>.ValidationError("request make name is required");
            }

            var existingMake = await _unitOfWork.GetMakesRepository.ExistsByNameAsync(request.MakeDto.Name);
            
            if (existingMake)
            {
                return BaseResponse<Unit>.Conflict($"Make with name: {request.MakeDto.Name} already exists");
            }

            var makeMapped = new Make(request.MakeDto.Name);

            await _unitOfWork.GetRepository<Make>().AddAsync(makeMapped);
            
            await _unitOfWork.SaveChangesAsync();
            
            return BaseResponse<Unit>.Created($"Make with name: {request.MakeDto.Name} is successfully created");
        }
        catch (Exception e)
        {
            _logger.LogError("An error occured while creating Make: {error}", e.Message);
            return BaseResponse<Unit>.InternalError($"An error occured while creating Make: {e.Message}");
        }
    }
}