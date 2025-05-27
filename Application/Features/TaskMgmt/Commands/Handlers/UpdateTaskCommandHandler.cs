using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using MediatR;

namespace Application.Features.TaskMgmt.Commands.Handlers;

public class UpdateTaskCommandHandler: IRequestHandler<UpdateTaskCommand, TaskItem>
{
    private readonly ITaskMgmtService _taskMgmtService;
    
    public UpdateTaskCommandHandler(ITaskMgmtService taskMgmtService)
    {
        _taskMgmtService = taskMgmtService;
    }

    public async Task<TaskItem> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        var task = new TaskItem
        {
            Id = request.taskId,
            Title = request.Title,
            Status = request.Status,
            Description = request.Description,
            AssignedToUserId = request.AssignedUserId,
            CreatedByUserId = request.CreatedByUserId,
            TeamId = request.TeamId,
            DueDate = request.DueDate,
        };
        return await _taskMgmtService.UpdateTaskAsync(task);
    }
}