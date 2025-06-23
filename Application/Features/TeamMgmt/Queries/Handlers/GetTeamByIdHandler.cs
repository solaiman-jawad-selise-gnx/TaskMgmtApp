using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using MediatR;

namespace Application.Features.TeamMgmt.Queries.Handlers;

public class GetTeamByIdHandler: IRequestHandler<GetTeamByIdQuery, Team?>
{
    private readonly ITeamMgmtService _service;

    public GetTeamByIdHandler(ITeamMgmtService service)
    {
        _service = service;
    }

    public async Task<Team?> Handle(GetTeamByIdQuery request, CancellationToken cancellationToken)
    {
        return await _service.GetTeamByIdAsync(request.TeamId);
    }
    
    
}