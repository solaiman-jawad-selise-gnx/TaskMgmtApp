using FluentValidation;

namespace Application.Features.TaskMgmt.Queries.Validators;

public class GetTasksValidator: AbstractValidator<GetTasksQuery>
{
    public GetTasksValidator()
    {
        RuleFor(x => x.parameters.Page)
            .GreaterThan(0).WithMessage("Page number must be greater than 0.");

        RuleFor(x => x.parameters.PageSize)
            .GreaterThan(0).WithMessage("Page size must be greater than 0.");
    }
}