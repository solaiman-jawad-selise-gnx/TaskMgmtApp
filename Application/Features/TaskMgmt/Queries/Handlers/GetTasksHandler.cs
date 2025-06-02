using Application.Interfaces;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.TaskMgmt.Queries.Handlers;

public class GetTasksHandler: IRequestHandler<GetTasksQuery, IEnumerable<TaskItem>>
{
    private readonly ITaskMgmtService _taskMgmtService;
    private readonly IValidator<GetTasksQuery> _validator;
    
    public GetTasksHandler(ITaskMgmtService taskMgmtService, IValidator<GetTasksQuery> validator)
    {
        _validator = validator;
        _taskMgmtService = taskMgmtService;
    }
    
    public async Task<IEnumerable<TaskItem>> Handle(GetTasksQuery request, CancellationToken cancellationToken)
    {
        // Validate the request
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        // Get the tasks based on the query parameters
        return await _taskMgmtService.GetTasksAsync(request.parameters);
    }
    

}