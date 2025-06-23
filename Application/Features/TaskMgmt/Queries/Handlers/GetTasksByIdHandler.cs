using Application.Interfaces;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.TaskMgmt.Queries.Handlers;

public class GetTasksByIdHandler: IRequestHandler<GetTaskByIdQuery, TaskItem?>
{
    private readonly ITaskMgmtService _taskMgmtService;
    private readonly IValidator<GetTaskByIdQuery> _validator;
    
    public GetTasksByIdHandler(ITaskMgmtService taskMgmtService, IValidator<GetTaskByIdQuery> validator)
    {
        _taskMgmtService = taskMgmtService;
        _validator = validator;
    }
    
    public async Task<TaskItem?> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
    {
        // Validate the request
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        // Retrieve the task item by ID
        return await _taskMgmtService.GetTaskByIdAsync(request.TaskId);
    }
}