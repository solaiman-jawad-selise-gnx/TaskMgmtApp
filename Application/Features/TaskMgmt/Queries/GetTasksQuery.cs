using Application.QueryParams;
using Domain.Entities;
using MediatR;

namespace Application.Features.TaskMgmt.Queries;

public class GetTasksQuery : IRequest<IEnumerable<TaskItem>>
{
    public TaskQueryParameters parameters {get; set; }
}