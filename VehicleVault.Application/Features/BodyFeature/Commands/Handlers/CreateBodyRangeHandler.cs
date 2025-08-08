using MediatR;
using Microsoft.Extensions.Logging;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.BodyFeature.Commands.Requests;
using VehicleVault.Application.Features.BodyFeature.DTOs;
using VehicleVault.Domain.Entities;
using VehicleVault.Infrastructure.UOF;

namespace VehicleVault.Application.Features.BodyFeature.Commands.Handlers;

public class CreateBodyRangeHandler: IRequestHandler<CreateBodyRangeRequest, BaseResponse<Unit>>
{
    #region Instance Fields
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateBodyRangeHandler> _logger;
    #endregion

    #region Constructor
    public CreateBodyRangeHandler(IUnitOfWork unitOfWork, ILogger<CreateBodyRangeHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    #endregion
    
    
    public async Task<BaseResponse<Unit>> Handle(CreateBodyRangeRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var validBodyToAdd = new List<CreateBodyDto>();
            var invalidBodyToRemove = new List<string>();
            
            foreach (var body in request.BodiesDto)
            {
                if (string.IsNullOrWhiteSpace(body.Name))
                {
                    _logger.LogError("Invalid body name: {makeName}", body.Name);
                    invalidBodyToRemove.Add(body.Name);
                    continue;
                }
                
                var isMakeExist = await _unitOfWork.GetMakesRepository.ExistsByNameAsync(body.Name);

                if (isMakeExist)
                {
                    _logger.LogError("Body is already exists: {bodyName}", body.Name);
                    invalidBodyToRemove.Add(body.Name);
                    continue;
                }

                else
                {
                    validBodyToAdd.Add(body);
                }
            }

            var bodyMapped = validBodyToAdd.Select(b => new Body(b.Name));
            
            if (validBodyToAdd.Count == 0)
            {
                _logger.LogWarning("No valid body to add");
                return BaseResponse<Unit>.BadRequest("No valid body to add");
            }

            if (invalidBodyToRemove.Count != 0)
            {
                _logger.LogWarning("some invalid bodies not added: {invalidMakes}", invalidBodyToRemove.ToList());
            }

            await _unitOfWork.GetRepository<Body>().AddRangeAsync(bodyMapped);
            await _unitOfWork.SaveChangesAsync();
            
            return BaseResponse<Unit>.Success($"All bodies were created: {validBodyToAdd.Count} / {request.BodiesDto.Count()}");
        }
        catch (Exception e)
        {
            _logger.LogError("An error occured while creating Body: {error}", e.Message);
            return BaseResponse<Unit>.InternalError($"An error occured while creating Body: {e.Message}");
        }
    }
}