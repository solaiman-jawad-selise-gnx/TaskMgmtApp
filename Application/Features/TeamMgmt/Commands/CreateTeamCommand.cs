using Domain.Entities;
using MediatR;

namespace Application.Features.TeamMgmt.Commands;

public class CreateTeamCommand: IRequest<Team>
{
    public string Name { get; set; }
    public string Description { get; set; }
}