using FluentValidation;

namespace Application.Features.TeamMgmt.Commands.Validators;

public class UpdateTeamValidator: AbstractValidator<UpdateTeamCommand>
{
    public UpdateTeamValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Team ID is required.")
            .GreaterThan(0).WithMessage("Team ID must be a positive integer.");

        RuleFor(x => x.TeamName)
            .NotEmpty().WithMessage("Team name is required.")
            .MaximumLength(100).WithMessage("Team name cannot exceed 100 characters.");

        RuleFor(x => x.TeamDescription)
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");
    }
}