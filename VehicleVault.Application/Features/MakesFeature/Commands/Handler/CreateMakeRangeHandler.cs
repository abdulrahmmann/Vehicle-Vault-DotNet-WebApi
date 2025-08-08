using MediatR;
using Microsoft.Extensions.Logging;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.MakesFeature.Commands.Requests;
using VehicleVault.Application.Features.MakesFeature.DTOs;
using VehicleVault.Domain.Entities;
using VehicleVault.Infrastructure.UOF;

namespace VehicleVault.Application.Features.MakesFeature.Commands.Handler;

public class CreateMakeRangeHandler: IRequestHandler<CreateMakesRangeRequest, BaseResponse<Unit>>
{
    #region Instance Fields
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateMakeRangeHandler> _logger;
    #endregion

    #region Constructor
    public CreateMakeRangeHandler(IUnitOfWork unitOfWork, ILogger<CreateMakeRangeHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    #endregion

    public async Task<BaseResponse<Unit>> Handle(CreateMakesRangeRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var validMakesToAdd = new List<CreateMakeDto>();
            var invalidMakesToRemove = new List<string>();

            foreach (var make in request.MakesDto)
            {
                if (string.IsNullOrWhiteSpace(make.Name))
                {
                    _logger.LogError("Invalid make name: {makeName}", make.Name);
                    invalidMakesToRemove.Add(make.Name);
                    continue;
                }

                var isMakeExist = await _unitOfWork.GetMakesRepository.ExistsByNameAsync(make.Name);

                if (isMakeExist)
                {
                    _logger.LogError("Make already exists: {makeName}", make.Name);
                    invalidMakesToRemove.Add(make.Name);
                    continue;
                }

                else
                {
                    validMakesToAdd.Add(make);
                }
            }

            var makesMapped = validMakesToAdd.Select(m => new Make(m.Name));
            
            if (validMakesToAdd.Count == 0)
            {
                _logger.LogWarning("No valid makes to add");
                return BaseResponse<Unit>.BadRequest("No valid makes to add");
            }

            if (invalidMakesToRemove.Count != 0)
            {
                _logger.LogWarning("some invalid makes not added: {invalidMakes}", invalidMakesToRemove.ToList());
            }

            await _unitOfWork.GetRepository<Make>().AddRangeAsync(makesMapped);
            await _unitOfWork.SaveChangesAsync();
            
            if (validMakesToAdd.Count != 0 && invalidMakesToRemove.Count != 0)
            {
                return BaseResponse<Unit>
                    .Success($"{validMakesToAdd.Count} valid makes added and {invalidMakesToRemove.Count} invalid makes");
            }
            
            return BaseResponse<Unit>.Success($"All makes were created: {validMakesToAdd.Count} / {request.MakesDto.Count()}");
        }
        catch (Exception e)
        {
            _logger.LogError("An error occured while creating Make: {error}", e.Message);
            return BaseResponse<Unit>.InternalError($"An error occured while creating Make: {e.Message}");
        }
    }
}