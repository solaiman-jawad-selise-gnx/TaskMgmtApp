using FluentValidation;

namespace Application.Features.UserMgmt.Commands.Validators;

public class DeleteUserValidator: AbstractValidator<DeleteUserCommand>
{
    public DeleteUserValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("User ID is required.")
            .GreaterThan(0).WithMessage("User ID must be a positive integer.");
    }
}