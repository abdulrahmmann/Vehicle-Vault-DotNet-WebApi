using FluentValidation;
using VehicleVault.Application.Features.VehiclesFeature.DTOs;

namespace VehicleVault.Application.Features.VehiclesFeature.Validation;

public class CreateVehicleValidator: AbstractValidator<CreateVehicleDto>
{
    public CreateVehicleValidator()
    {
        RuleFor(v => v.Name)
            .NotEmpty().WithMessage("Name is required")
            .NotNull().WithMessage("Name is required")
            .MaximumLength(150).WithMessage("Name must not exceed 150 characters");
        
        RuleFor(v => v.Description)
            .NotEmpty().WithMessage("Description is required")
            .NotNull().WithMessage("Description is required")
            .MaximumLength(2000).WithMessage("Description must not exceed 2000 characters");
        
        RuleFor(v => v.Engine)
            .NotEmpty().WithMessage("Engine is required")
            .NotNull().WithMessage("Engine is required")
            .MaximumLength(100).WithMessage("Engine must not exceed 100 characters");

        RuleFor(v => v.EngineCc)
            .NotEmpty().WithMessage("EngineCc is required")
            .NotNull().WithMessage("EngineCc is required");
        
        RuleFor(v => v.EngineCylinders)
            .NotEmpty().WithMessage("EngineCylinders is required")
            .NotNull().WithMessage("EngineCylinders is required");
        
        RuleFor(v => v.EngineLiterDisplay)
            .NotEmpty().WithMessage("EngineLiterDisplay is required")
            .NotNull().WithMessage("EngineLiterDisplay is required");
        
        RuleFor(v => v.Year)
            .NotEmpty().WithMessage("Year is required")
            .NotNull().WithMessage("Year is required");
        
        RuleFor(v => v.NumDoors)
            .NotEmpty().WithMessage("NumDoors is required")
            .NotNull().WithMessage("NumDoors is required");
        
        
    }
}