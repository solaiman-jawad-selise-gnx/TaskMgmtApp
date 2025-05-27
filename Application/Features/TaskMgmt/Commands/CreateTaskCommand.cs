using Domain.Entities;
using MediatR;

namespace Application.Features.TaskMgmt.Commands;

public class CreateTaskCommand : IRequest<TaskItem>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int AssignedUserId { get; set; }
    public int CreatedByUserId { get; set; }
    public int TeamId { get; set; }
    public DateTime DueDate { get; set; }
}