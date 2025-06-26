using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.TeamMgmt.Commands.Handler;

public class DeleteTeamCommandHandler: IRequestHandler<DeleteTeamCommand, Team?>
{
    
    private readonly ITeamMgmtService _teamMgmtService;
    
    public DeleteTeamCommandHandler(ITeamMgmtService teamMgmtService)
    {
        _teamMgmtService = teamMgmtService;
    }

    public async Task<Team?> Handle(DeleteTeamCommand request, CancellationToken cancellationToken)
    {
        var deletedTeam = await _teamMgmtService.DeleteTeamAsync(request.Id);
        return deletedTeam;
    }
}