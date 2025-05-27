using Domain.Enums;

namespace Domain.Entities;

public class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public TaskMgmtStatus Status { get; set; }
    public DateTime DueDate { get; set; }
    public User AssignedToUser { get; set; }
    public int AssignedToUserId { get; set; }


    public User CreatedByUser { get; set; }

    public int CreatedByUserId { get; set; }

    public Team Team { get; set; }
    public int TeamId { get; set; }
}