using FluentValidation;
using VehicleVault.Application.Features.CategoryFeature.DTOs;

namespace VehicleVault.Application.Features.CategoryFeature.Validation;

public class CategoryValidator: AbstractValidator<CategoryDto>
{
    public CategoryValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required and cannot be required")
            .NotNull().WithMessage("Name is required and cannot be null")
            .MaximumLength(30).WithMessage("Name must not exceed 30 characters");
        
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required and cannot be required")
            .NotNull().WithMessage("Description is required and cannot be null")
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters");
    }
}