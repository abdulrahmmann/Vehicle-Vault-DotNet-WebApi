using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.CategoryFeature.Commands.Requests;
using VehicleVault.Application.Features.CategoryFeature.DTOs;
using VehicleVault.Domain.Entities;
using VehicleVault.Infrastructure.UOF;

namespace VehicleVault.Application.Features.CategoryFeature.Commands.Handler;

public class CreateCategoriesRangeHandler: IRequestHandler<CreateCategoriesRangeRequest, BaseResponse<Unit>>
{
    #region Instance Fields
    private readonly IUnitOfWork  _unitOfWork;
    private readonly IValidator<CategoryDto> _validator;
    private readonly ILogger<CreateCategoriesRangeHandler> _logger;
    #endregion

    #region Constructor
    public CreateCategoriesRangeHandler(IUnitOfWork unitOfWork, IValidator<CategoryDto> validator, 
        ILogger<CreateCategoriesRangeHandler> logger) 
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _validator = validator;
    }
    #endregion

    public async Task<BaseResponse<Unit>> Handle(CreateCategoriesRangeRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var validCategoriesToAdd = new List<CategoryDto>();
            var invalidCategoriesToRemove = new List<string>();
            
            foreach (var category in request.CategoriesDto)
            {
                var isCategoryValid = await IsCategoryValidAsync(category, cancellationToken);

                if (!isCategoryValid)
                {
                    invalidCategoriesToRemove.Add(category.Name);
                    continue;
                }
                // add valid categories
                validCategoriesToAdd.Add(category);
            }
            
            var enumerableCategories = validCategoriesToAdd.AsEnumerable();
            
            var categoriesMapped = enumerableCategories
                .Select(c => new Category(c.Name, c.Description));
            
            if (validCategoriesToAdd.Count == 0)
            {
                _logger.LogWarning("No valid categories to add");
                return BaseResponse<Unit>.BadRequest("No valid categories to add");
            }

            if (invalidCategoriesToRemove.Count != 0)
            {
                _logger.LogWarning("some invalid categories not added: {invalidCategories}", invalidCategoriesToRemove.ToList());
            }
            
            await _unitOfWork.GetRepository<Category>().AddRangeAsync(categoriesMapped);
            await _unitOfWork.SaveChangesAsync();

            if (validCategoriesToAdd.Count != 0 && invalidCategoriesToRemove.Count != 0)
            {
                return BaseResponse<Unit>
                    .Success($"{validCategoriesToAdd.Count} valid categories added and {invalidCategoriesToRemove.Count} invalid categories");
            }
            
            return BaseResponse<Unit>.Success($"All Categories were created: {validCategoriesToAdd.Count} / {request.CategoriesDto.Count()}");
        }
        catch (Exception e)
        {
            _logger.LogError("An error occured while creating category: {error}", e.Message);
            return BaseResponse<Unit>.InternalError($"An error occured while creating category: {e.Message}");
        }
    }
    
    private async Task<bool> IsCategoryValidAsync(CategoryDto category, CancellationToken ct)
    {
        var validationResult = await _validator.ValidateAsync(category, ct);
        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Validation error for {Category}: {Errors}",
                category.Name,
                string.Join(",", validationResult.Errors.Select(e => e.ErrorMessage)));
            return false;
        }

        if (await _unitOfWork.GetCategoryRepository.ExistsByNameAsync(category.Name))
        {
            _logger.LogWarning("Category already exists: {Name}", category.Name);
            return false;
        }

        return true;
    }
}