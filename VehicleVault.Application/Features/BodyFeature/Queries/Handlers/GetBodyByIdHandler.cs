using MediatR;
using Microsoft.Extensions.Logging;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.BodyFeature.DTOs;
using VehicleVault.Application.Features.BodyFeature.Queries.Requests;
using VehicleVault.Domain.Entities;
using VehicleVault.Infrastructure.UOF;

namespace VehicleVault.Application.Features.BodyFeature.Queries.Handlers;

public class GetBodyByIdHandler: IRequestHandler<GetBodyByIdRequest, BaseResponse<BodyDto>>
{
    #region Instance Fields
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetBodyByIdHandler> _logger;
    #endregion

    #region Constructor
    public GetBodyByIdHandler(IUnitOfWork unitOfWork, ILogger<GetBodyByIdHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    #endregion

    public async Task<BaseResponse<BodyDto>> Handle(GetBodyByIdRequest request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.Id <= 0)
            {
                return BaseResponse<BodyDto>.ValidationError("Id must be greater than zero");
            }

            var body = await _unitOfWork.GetRepository<Body>().GetByIdAsync(request.Id);

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