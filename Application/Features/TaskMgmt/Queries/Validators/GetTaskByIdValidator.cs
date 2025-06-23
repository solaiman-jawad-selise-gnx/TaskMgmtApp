using FluentValidation;

namespace Application.Features.TaskMgmt.Queries.Validators;

public class GetTaskByIdValidator : AbstractValidator<GetTaskByIdQuery>
{
    public GetTaskByIdValidator()
    {
        RuleFor(x => x.TaskId)
            .NotEmpty().WithMessage("Task ID is required.")
            .GreaterThan(0).WithMessage("Task ID must be a positive integer.");
    }
}