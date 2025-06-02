using Application.Interfaces;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.TaskMgmt.Commands.Handlers;

public class UpdateTaskCommandHandler: IRequestHandler<UpdateTaskCommand, TaskItem>
{
    private readonly ITaskMgmtService _taskMgmtService;
    private readonly IValidator<UpdateTaskCommand> _validator;
    public UpdateTaskCommandHandler(ITaskMgmtService taskMgmtService, IValidator<UpdateTaskCommand> validator)
    {
        _taskMgmtService = taskMgmtService;
        _validator = validator;
    }

    public async Task<TaskItem> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        // Validate the request
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        // Update the task item
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