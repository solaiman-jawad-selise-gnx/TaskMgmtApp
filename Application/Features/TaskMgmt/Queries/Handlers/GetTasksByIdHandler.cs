using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.TaskMgmt.Queries.Handlers;

public class GetTasksByIdHandler: IRequestHandler<GetTaskByIdQuery, TaskItem?>
{
    private readonly ITaskMgmtService _taskMgmtService;
    
    public GetTasksByIdHandler(ITaskMgmtService taskMgmtService)
    {
        _taskMgmtService = taskMgmtService;
    }
    
    public async Task<TaskItem?> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
    {
        return await _taskMgmtService.GetTaskByIdAsync(request.TaskId);
    }
}