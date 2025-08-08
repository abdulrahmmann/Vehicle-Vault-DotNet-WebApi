using MediatR;
using Microsoft.Extensions.Logging;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.MakesFeature.DTOs;
using VehicleVault.Application.Features.MakesFeature.Queries.Requests;
using VehicleVault.Domain.Entities;
using VehicleVault.Infrastructure.UOF;

namespace VehicleVault.Application.Features.MakesFeature.Queries.Handler;

public class GetMakeByIdHandler: IRequestHandler<GetMakeByIdRequest, BaseResponse<MakeDto>>
{
    #region Instance Fields
    private readonly IUnitOfWork  _unitOfWork;
    private readonly ILogger<GetMakeByIdHandler> _logger;
    #endregion
    
    #region Constructor
    public GetMakeByIdHandler(IUnitOfWork unitOfWork, ILogger<GetMakeByIdHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    #endregion

    public async Task<BaseResponse<MakeDto>> Handle(GetMakeByIdRequest request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.Id <= 0)
            {
                return BaseResponse<MakeDto>.ValidationError("Id must be greater than zero");
            }

            var make = await _unitOfWork.GetRepository<Make>().GetByIdAsync(request.Id);

            if (make == null)
            {
                return BaseResponse<MakeDto>.NotFound("Make not found");
            }

            var makeMapped = new MakeDto(make.Id, make.Name);
            
            return BaseResponse<MakeDto>.Success(makeMapped, $"make by Id: {request.Id} retrieved successfully", 1);
        }
        catch (Exception e)
        {
            _logger.LogError("An error occured while Retrieving Make By Id: {error}", e.Message);
            return BaseResponse<MakeDto>.InternalError($"An error occured while Retrieving Make By Id: {e.Message}");
        }
    }
}