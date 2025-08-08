using MediatR;
using Microsoft.Extensions.Logging;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.MakesFeature.DTOs;
using VehicleVault.Application.Features.MakesFeature.Queries.Requests;
using VehicleVault.Infrastructure.UOF;

namespace VehicleVault.Application.Features.MakesFeature.Queries.Handler;

public class GetMakeByNameHandler: IRequestHandler<GetMakeByNameRequest, BaseResponse<MakeDto>>
{
    #region Instance Fields
    private readonly IUnitOfWork  _unitOfWork;
    private readonly ILogger<GetMakeByNameHandler> _logger;
    #endregion
    
    #region Constructor
    public GetMakeByNameHandler(IUnitOfWork unitOfWork, ILogger<GetMakeByNameHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    #endregion

    public async Task<BaseResponse<MakeDto>> Handle(GetMakeByNameRequest request, CancellationToken cancellationToken)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(request.MakeName))
            {
                return BaseResponse<MakeDto>.ValidationError("Make Name is required");
            }

            var make = await _unitOfWork.GetMakesRepository.GetMakeByName(request.MakeName);

            if (make == null)
            {
                return BaseResponse<MakeDto>.NotFound("Make not found");
            }

            var makeMapped = new MakeDto(make.Id, make.Name);
            
            return BaseResponse<MakeDto>.Success(makeMapped, $"make by Name: {request.MakeName} retrieved successfully", 1);
        }
        catch (Exception e)
        {
            _logger.LogError("An error occured while Retrieving Make By Name: {error}", e.Message);
            return BaseResponse<MakeDto>.InternalError($"An error occured while Retrieving Make By Name: {e.Message}");
        }
    }
}