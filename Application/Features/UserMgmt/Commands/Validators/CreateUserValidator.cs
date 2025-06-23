using Domain.Enums;
using FluentValidation;

namespace Application.Features.UserMgmt.Commands.Validators;

public class CreateUserValidator: AbstractValidator<CreateUserCommand>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");
        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("Full name is required.")
            .MaximumLength(100).WithMessage("Full name must not exceed 100 characters.");
        RuleFor(x => x.Role)
            .NotEmpty().WithMessage("Role is required.")
            .Must(role => Enum.GetNames(typeof(Role)).Contains(role))
            .WithMessage("Role must be a valid role.");
    }
}