using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using FluentValidation;
using MediatR;

namespace Application.Features.TaskMgmt.Commands.Handlers;

public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, TaskItem>
{
    private readonly ITaskMgmtService _service;
    private readonly IValidator<CreateTaskCommand> _validator;

    public CreateTaskCommandHandler(ITaskMgmtService service, IValidator<CreateTaskCommand> validator)
    {
        _service = service;
        _validator = validator;
    }

    public async Task<TaskItem> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        // Validate the request
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        // Create the task item
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