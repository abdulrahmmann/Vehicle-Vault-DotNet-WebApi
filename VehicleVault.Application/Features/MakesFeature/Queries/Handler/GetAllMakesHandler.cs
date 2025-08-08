using MediatR;
using Microsoft.Extensions.Logging;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.MakesFeature.DTOs;
using VehicleVault.Application.Features.MakesFeature.Queries.Requests;
using VehicleVault.Domain.Entities;
using VehicleVault.Infrastructure.UOF;

namespace VehicleVault.Application.Features.MakesFeature.Queries.Handler;

public class GetAllMakesHandler: IRequestHandler<GetAllMakesRequest, BaseResponse<IEnumerable<MakeDto>>>
{
    #region Instance Fields
    private readonly IUnitOfWork  _unitOfWork;
    private readonly ILogger<GetAllMakesHandler> _logger;
    #endregion
    
    #region Constructor
    public GetAllMakesHandler(IUnitOfWork unitOfWork, ILogger<GetAllMakesHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    #endregion

    public async Task<BaseResponse<IEnumerable<MakeDto>>> Handle(GetAllMakesRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var makes = await _unitOfWork.GetRepository<Make>().GetAllAsync();

            var enumerable = makes.ToList();
            
            if (enumerable.Count == 0)
            {
                _logger.LogError("No Makes found");
                return BaseResponse<IEnumerable<MakeDto>>.NotFound("No Makes found");
            }

            var makesMapped = enumerable.Select(m => new MakeDto(m.Id, m.Name));
            
            return BaseResponse<IEnumerable<MakeDto>>.Success(makesMapped, "Makes Retrieved Successfully", enumerable.Count);
        }
        catch (Exception e)
        {
            _logger.LogError("An error occured while Retrieving Makes: {error}", e.Message);
            return BaseResponse<IEnumerable<MakeDto>>.InternalError($"An error occured while Retrieving Makes: {e.Message}");
        }
    }
}