using FluentValidation;

namespace Application.Features.TaskMgmt.Commands.Validators;

public class DeleteTaskValidator: AbstractValidator<DeleteTaskCommand>
{
    public DeleteTaskValidator()
    {
        RuleFor(x => x.TaskId)
            .NotEmpty().WithMessage("Task ID is required.")
            .GreaterThan(0).WithMessage("Task ID must be a positive integer.");
    }
}