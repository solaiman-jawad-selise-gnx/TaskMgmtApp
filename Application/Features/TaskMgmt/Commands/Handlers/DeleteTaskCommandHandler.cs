using Application.Interfaces;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.TaskMgmt.Commands.Handlers;

public class DeleteTaskCommandHandler: IRequestHandler<DeleteTaskCommand, TaskItem?>
{
   private readonly ITaskMgmtService _service;
   private readonly IValidator<DeleteTaskCommand> _validator;
   
   public DeleteTaskCommandHandler(ITaskMgmtService service, IValidator<DeleteTaskCommand> validator)
   {
       _service = service;
       _validator = validator;
   }
   
    public async Task<TaskItem?> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        // Validate the request
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        // Delete the task item
         var task = await _service.DeleteTaskAsync(request.TaskId);
         return task;
    }
}