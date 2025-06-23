using Domain.Enums;
using FluentValidation;

namespace Application.Features.UserMgmt.Commands.Validators;

public class UpdateUserValidator: AbstractValidator<UpdateUserCommand>
{
    public UpdateUserValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("user ID is required.")
            .GreaterThan(0).WithMessage("user ID must be a positive integer."); 
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("email is required.")
            .EmailAddress().WithMessage("email address must be a valid email address.");
        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("full name is required.")
            .MaximumLength(100).WithMessage("full name must not exceed 100 characters.");
        RuleFor(x => x.Role)
            .NotEmpty().WithMessage("Role is required.")
            .Must(role => Enum.GetNames(typeof(Role)).Contains(role))
            .WithMessage("Role must be a valid role.");
    }
}