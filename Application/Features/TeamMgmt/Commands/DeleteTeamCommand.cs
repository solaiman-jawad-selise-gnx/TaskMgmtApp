using Domain.Entities;
using MediatR;

namespace Application.Features.TeamMgmt.Commands;

public class DeleteTeamCommand: IRequest<Team?>
{
    public int Id { get; set; }
}