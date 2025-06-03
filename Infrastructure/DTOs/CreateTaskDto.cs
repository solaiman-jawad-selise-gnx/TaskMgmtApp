namespace Infrastructure.DTOs;

public class CreateTaskDto
{
    public string Title { get; set; }
    
    public string Description { get; set; }
    
    public int AssignedUserId { get; set; }
    
    public int CreatedByUserId { get; set; }
    
    public int TeamId { get; set; }
    
    public DateTime DueDate { get; set; }
}