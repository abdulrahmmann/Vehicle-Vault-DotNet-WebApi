using FluentValidation;
using VehicleVault.Application.Features.UserFeature.DTOs;

namespace VehicleVault.Application.Features.UserFeature.Validations;

public class RegisterUserDtoByAdminValidator: AbstractValidator<RegisterUserDtoByAdmin>
{
    public RegisterUserDtoByAdminValidator()
    {
        RuleFor(user => user.UserName)
            .MinimumLength(6).WithMessage("UserName name must be at least 6 characters long.")
            .MaximumLength(60).WithMessage("UserName name cannot exceed 60 characters.")
            .NotEmpty().WithMessage("UserName must not be empty.")
            .NotNull().WithMessage("UserName must not be null");
        
        RuleFor(user => user.Email)
            .EmailAddress().WithMessage("Invalid email format")
            .NotEmpty().WithMessage("Email must not be empty.")
            .NotNull().WithMessage("Email must not be null");
        
        RuleFor(user => user.PhoneNumber)
            .NotEmpty().WithMessage("PhoneNumber must not be empty.")
            .NotNull().WithMessage("PhoneNumber must not be null");
        
        RuleFor(user => user.Password)
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .MaximumLength(100).WithMessage("Password cannot exceed 100 characters.")
            .NotEmpty().WithMessage("Password must not be empty.")
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$")
            .WithMessage("Password must contain uppercase, lowercase, number, and special character.");
        
        RuleFor(user => user.ConfirmPassword)
            .MinimumLength(8).WithMessage("Confirm Password must be at least 8 characters long.")
            .MaximumLength(100).WithMessage("Confirm Password cannot exceed 100 characters.")
            .NotEmpty().WithMessage("Confirm Password must not be empty.")
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$")
            .WithMessage("Confirm Password must contain uppercase, lowercase, number, and special character.")
            .Equal(user => user.Password).WithMessage("Password and Confirm Password does not matched.");
    }
}