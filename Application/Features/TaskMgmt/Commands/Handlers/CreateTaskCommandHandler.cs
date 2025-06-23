using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Features.TaskMgmt.Commands.Handlers;

public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, TaskItem>
{
    private readonly ITaskMgmtService _service;

    public CreateTaskCommandHandler(ITaskMgmtService service)
    {
        _service = service;
    }

    public async Task<TaskItem> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var task = new TaskItem
        {
            Title = request.Title,
            Description = request.Description,
            Status = TaskMgmtStatus.Todo,
            AssignedToUserId = request.AssignedUserId,
            CreatedByUserId = request.CreatedByUserId,
            TeamId = request.TeamId,
            DueDate = request.DueDate,
            
        };
        return await _service.CreateTaskAsync(task);
    }
}