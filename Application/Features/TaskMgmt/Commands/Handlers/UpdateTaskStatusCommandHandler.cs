using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using MediatR;

namespace Application.Features.TaskMgmt.Commands.Handlers;

public class UpdateTaskStatusCommandHandler: IRequestHandler<UpdateTaskStatusCommand, TaskItem?>
{
    private readonly ITaskMgmtService _taskMgmtService;

    public UpdateTaskStatusCommandHandler(ITaskMgmtService taskService)
    {
        _taskMgmtService = taskService;
    }

    public async Task<TaskItem?> Handle(UpdateTaskStatusCommand request, CancellationToken cancellationToken)
    {
        return await _taskMgmtService.UpdateTaskStatusAsync(request.TaskId, request.Status);
    }
}