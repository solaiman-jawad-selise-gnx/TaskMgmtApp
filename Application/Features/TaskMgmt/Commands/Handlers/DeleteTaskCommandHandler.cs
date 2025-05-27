using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.TaskMgmt.Commands.Handlers;

public class DeleteTaskCommandHandler: IRequestHandler<DeleteTaskCommand, TaskItem?>
{
   private readonly ITaskMgmtService _service;
   
   public DeleteTaskCommandHandler(ITaskMgmtService service)
   {
       _service = service;
   }
   
    public async Task<TaskItem?> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
         var task = await _service.DeleteTaskAsync(request.TaskId);
         return task;
    }
}