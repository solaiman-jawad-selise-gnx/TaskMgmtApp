using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.TaskMgmt.Queries.Handlers;

public class GetTasksHandler: IRequestHandler<GetTasksQuery, IEnumerable<TaskItem>>
{
    private readonly ITaskMgmtService _taskMgmtService;
    
    public GetTasksHandler(ITaskMgmtService taskMgmtService)
    {
        _taskMgmtService = taskMgmtService;
    }
    
    public async Task<IEnumerable<TaskItem>> Handle(GetTasksQuery request, CancellationToken cancellationToken)
    {
        return await _taskMgmtService.GetTasksAsync(request.parameters);
    }
    

}