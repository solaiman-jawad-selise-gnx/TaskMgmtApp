using Domain.Entities;
using MediatR;

namespace Application.Features.TaskMgmt.Queries;

public class GetTaskByIdQuery: IRequest<TaskItem?>
{
    public int TaskId { get; set; }
    
}