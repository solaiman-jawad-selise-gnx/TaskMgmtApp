using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Features.TaskMgmt.Commands;

public class UpdateTaskStatusCommand: IRequest<TaskItem>
{
    public int TaskId { get; set; }
    public TaskMgmtStatus Status { get; set; }
}