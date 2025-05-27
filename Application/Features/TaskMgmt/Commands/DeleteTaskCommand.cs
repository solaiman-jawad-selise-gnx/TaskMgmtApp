using Domain.Entities;
using MediatR;

namespace Application.Features.TaskMgmt.Commands;

public class DeleteTaskCommand : IRequest<TaskItem?>
{
    public int TaskId { get; set; }
}