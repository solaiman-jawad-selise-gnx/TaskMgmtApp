using Domain.Enums;
using FluentValidation;

namespace Application.Features.TaskMgmt.Commands.Validators;


public class UpdateTaskStatusValidator : AbstractValidator<UpdateTaskStatusCommand>
{
    public UpdateTaskStatusValidator()
    {
        RuleFor(x => x.TaskId)
            .NotEmpty().WithMessage("Task ID is required.")
            .GreaterThan(0).WithMessage("Task ID must be a positive integer.");

        RuleFor(x => x.Status)
            .NotEmpty().WithMessage("Status is required.")
            .IsInEnum().WithMessage("Invalid status value.");
    }
}