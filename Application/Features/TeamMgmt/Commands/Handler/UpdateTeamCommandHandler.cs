using Application.Interfaces;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.TeamMgmt.Commands.Handler;

public class UpdateTeamCommandHandler: IRequestHandler<UpdateTeamCommand, Team>
{
    private readonly ITeamMgmtService _teamMgmtService;
    private readonly IValidator<UpdateTeamCommand> _validator;

    public UpdateTeamCommandHandler(ITeamMgmtService teamMgmtService, IValidator<UpdateTeamCommand> validator)
    {
        _teamMgmtService = teamMgmtService;
        _validator = validator;
    }

    public async Task<Team> Handle(UpdateTeamCommand request, CancellationToken cancellationToken)
    {
        // Validate the request
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        // Update the team
        var team = new Team{
            Id = request.Id,
            Name = request.TeamName,
            Description = request.TeamDescription
        };
        return await _teamMgmtService.UpdateTeamAsync(team);
    }

}