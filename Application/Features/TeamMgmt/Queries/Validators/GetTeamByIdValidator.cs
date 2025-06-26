using FluentValidation;

namespace Application.Features.TeamMgmt.Queries.Validators;

public class GetTeamByIdValidator: AbstractValidator<GetTeamByIdQuery>
{
    public GetTeamByIdValidator()
    {
        RuleFor(x => x.TeamId)
            .NotEmpty().WithMessage("Team ID is required.")
            .GreaterThan(0).WithMessage("Team ID must be a positive integer.");
    }
}