using MediatR;
using Microsoft.Extensions.Logging;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.BodyFeature.DTOs;
using VehicleVault.Application.Features.BodyFeature.Queries.Requests;
using VehicleVault.Domain.Entities;
using VehicleVault.Infrastructure.UOF;

namespace VehicleVault.Application.Features.BodyFeature.Queries.Handlers;

public class GetAllBodiesHandler: IRequestHandler<GetAllBodiesRequest, BaseResponse<IEnumerable<BodyDto>>>
{
    #region Instance Fields
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetAllBodiesHandler> _logger;
    #endregion

    #region Constructor
    public GetAllBodiesHandler(IUnitOfWork unitOfWork, ILogger<GetAllBodiesHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    #endregion
    
    
    public async Task<BaseResponse<IEnumerable<BodyDto>>> Handle(GetAllBodiesRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var bodies = await _unitOfWork.GetRepository<Body>().GetAllAsync();

            var enumerable = bodies.ToList();
            
            if (enumerable.Count == 0)
            {
                return BaseResponse<IEnumerable<BodyDto>>.NoContent("No Body found");
            }

            var bodiesMapped = enumerable.Select(b => new BodyDto(b.Id, b.Name));
            
            return BaseResponse<IEnumerable<BodyDto>>.Success(bodiesMapped, "All Bodies Retrieving Successfully", enumerable.Count);
        }
        catch (Exception e)
        {
            _logger.LogError("An error occured while Retrieving Bodies: {error}", e.Message);
            return BaseResponse<IEnumerable<BodyDto>>.InternalError($"An error occured while Retrieving Bodies: {e.Message}");
        }
    }
}