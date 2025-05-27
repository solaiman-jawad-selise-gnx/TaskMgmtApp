using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using MediatR;

namespace Application.Features.TeamMgmt.Commands.Handler;

public class CreateTeamCommandHandler: IRequestHandler<CreateTeamCommand, Team>
{
    private readonly ITeamMgmtService _teamMgmtService;
    
    public CreateTeamCommandHandler(ITeamMgmtService teamMgmtService)
    {
        _teamMgmtService = teamMgmtService;
    }
    
    public async Task<Team> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
    {
        var team = new Team
        {
            Name = request.Name,
            Description = request.Description,
        };

        return await _teamMgmtService.CreateTeamAsync(team);
    }
}