using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.CategoryFeature.Commands.Requests;
using VehicleVault.Application.Features.CategoryFeature.DTOs;
using VehicleVault.Domain.Entities;
using VehicleVault.Infrastructure.UOF;

namespace VehicleVault.Application.Features.CategoryFeature.Commands.Handler;

public class CreateCategoryHandler: IRequestHandler<CreateCategoryRequest, BaseResponse<Unit>>
{
    #region Instance Fields
    private readonly IUnitOfWork  _unitOfWork;
    private readonly IValidator<CategoryDto> _validator;
    private readonly ILogger<CreateCategoryHandler> _logger;
    #endregion

    #region Constructor
    public CreateCategoryHandler(IUnitOfWork unitOfWork, ILogger<CreateCategoryHandler> logger, IValidator<CategoryDto> validator)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _validator = validator;
    }
    #endregion

    public async Task<BaseResponse<Unit>> Handle(CreateCategoryRequest request, CancellationToken cancellationToken)
    {
        try
        {
            if (request == null) return BaseResponse<Unit>.BadRequest();

            var validationResult = await _validator.ValidateAsync(request.CategoryDto, cancellationToken);

            if (!validationResult.IsValid)
            {
                var validationErrors = string.Join(",", validationResult.Errors.Select(x => x.ErrorMessage));
                _logger.LogError(validationErrors);
                return BaseResponse<Unit>.ValidationError(validationErrors);
            }

            var existingCategory = await _unitOfWork.GetCategoryRepository.ExistsByNameAsync(request.CategoryDto.Name);

            if (existingCategory)
            {
                return BaseResponse<Unit>.Conflict($"Category with name: {request.CategoryDto.Name} already exists");
            }

            var categoryMapped = new Category(request.CategoryDto.Name, request.CategoryDto.Description);

            await _unitOfWork.GetCategoryRepository.AddAsync(categoryMapped);
            
            await _unitOfWork.SaveChangesAsync();
            
            return BaseResponse<Unit>.Created($"Category with name: {request.CategoryDto.Name} is successfully created");
        }
        catch (Exception e)
        {
            _logger.LogError("An error occured while creating category: {error}", e.Message);
            return BaseResponse<Unit>.InternalError($"An error occured while creating category: {e.Message}");
        }
    }
}