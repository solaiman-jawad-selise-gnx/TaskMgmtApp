using Application.Interfaces;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.TaskMgmt.Commands.Handlers;

public class UpdateTaskStatusCommandHandler: IRequestHandler<UpdateTaskStatusCommand, TaskItem?>
{
    private readonly ITaskMgmtService _taskMgmtService;
    private readonly IValidator<UpdateTaskStatusCommand> _validator;

    public UpdateTaskStatusCommandHandler(ITaskMgmtService taskService, IValidator<UpdateTaskStatusCommand> validator)
    {
        _taskMgmtService = taskService;
        _validator = validator;
    }

    public async Task<TaskItem?> Handle(UpdateTaskStatusCommand request, CancellationToken cancellationToken)
    {
        // Validate the request
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        // Update the task status
        return await _taskMgmtService.UpdateTaskStatusAsync(request.TaskId, request.Status);
    }
}