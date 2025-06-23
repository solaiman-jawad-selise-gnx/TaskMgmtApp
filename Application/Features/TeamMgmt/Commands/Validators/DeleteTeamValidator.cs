using FluentValidation;

namespace Application.Features.TeamMgmt.Commands.Validators;

public class DeleteTeamValidator: AbstractValidator<DeleteTeamCommand>
{
    public DeleteTeamValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Team ID is required.")
            .GreaterThan(0).WithMessage("Team ID must be a positive integer.");
    }
}