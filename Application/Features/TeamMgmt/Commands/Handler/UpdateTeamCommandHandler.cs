using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using MediatR;

namespace Application.Features.TeamMgmt.Commands.Handler;

public class UpdateTeamCommandHandler: IRequestHandler<UpdateTeamCommand, Team>
{
    private readonly ITeamMgmtService _teamMgmtService;

    public UpdateTeamCommandHandler(ITeamMgmtService teamMgmtService)
    {
        _teamMgmtService = teamMgmtService;
    }

    public async Task<Team> Handle(UpdateTeamCommand request, CancellationToken cancellationToken)
    {
        var team = new Team{
            Id = request.Id,
            Name = request.TeamName,
            Description = request.TeamDescription
        };
        return await _teamMgmtService.UpdateTeamAsync(team);
    }

}