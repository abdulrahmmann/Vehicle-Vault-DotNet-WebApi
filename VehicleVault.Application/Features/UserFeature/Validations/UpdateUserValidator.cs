using FluentValidation;
using VehicleVault.Application.Features.UserFeature.DTOs;

namespace VehicleVault.Application.Features.UserFeature.Validations;

public class UpdateUserValidator: AbstractValidator<UpdateUserDto>
{
    public UpdateUserValidator()
    {
        RuleFor(user => user.UserName)
            .MinimumLength(6).WithMessage("UserName name must be at least 6 characters long.")
            .MaximumLength(60).WithMessage("UserName name cannot exceed 60 characters.");

        RuleFor(user => user.Email)
            .EmailAddress().WithMessage("Invalid email format");
    }
}