using Domain.Entities;
using MediatR;

namespace Application.Features.TeamMgmt.Queries;

public class GetTeamByIdQuery: IRequest<Team>
{
    public int TeamId { get; set; }
    
}