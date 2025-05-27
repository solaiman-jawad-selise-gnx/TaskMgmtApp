using Domain.Entities;
using MediatR;

namespace Application.Features.TeamMgmt.Commands;

public class UpdateTeamCommand: IRequest<Team>
{
    public int Id { get; set; }
    public string TeamName { get; set; }
    public string TeamDescription { get; set; }
    
    
}