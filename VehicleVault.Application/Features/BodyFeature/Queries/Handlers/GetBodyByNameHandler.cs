using MediatR;
using Microsoft.Extensions.Logging;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.BodyFeature.DTOs;
using VehicleVault.Application.Features.BodyFeature.Queries.Requests;
using VehicleVault.Infrastructure.UOF;

namespace VehicleVault.Application.Features.BodyFeature.Queries.Handlers;

public class GetBodyByNameHandler: IRequestHandler<GetBodyByNameRequest, BaseResponse<BodyDto>>
{
    #region Instance Fields
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetBodyByNameHandler> _logger;
    #endregion

    #region Constructor
    public GetBodyByNameHandler(IUnitOfWork unitOfWork, ILogger<GetBodyByNameHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    #endregion

    public async Task<BaseResponse<BodyDto>> Handle(GetBodyByNameRequest request, CancellationToken cancellationToken)
    {
        try
        {
            if (string.IsNullOrEmpty(request.Name))
            {
                return BaseResponse<BodyDto>.ValidationError("Name is required");
            }

            var body = await _unitOfWork.GetBodyRepository.GetBodyByName(request.Name);

            if (body == null)
            {
                return BaseResponse<BodyDto>.NoContent("Body not found");
            }
            
            var bodyMapped = new BodyDto(body.Id, body.Name);
            
            return BaseResponse<BodyDto>.Success(bodyMapped,$"Body By Name: {body.Name} Retrieved Successfully", 1);
        }
        catch (Exception e)
        {
            _logger.LogError("An error occured while Retrieving Body By Name: {error}", e.Message);
            return BaseResponse<BodyDto>.InternalError($"An error occured while Retrieving Body By Name: {e.Message}");
        }
    }
}