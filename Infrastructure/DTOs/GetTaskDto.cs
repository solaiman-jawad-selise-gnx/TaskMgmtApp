namespace Infrastructure.DTOs;

public class GetTaskDto
{
      public int TaskId { get; set; }
      
      public string Title { get; set; }
      
      public string Description { get; set; }
      
      public int AssignedUserId { get; set; }
      
      public string AssignedUserName { get; set; }
      
      public string AssignedUserRole { get; set; }
      
      public int CreatedByUserId { get; set; }
      
      public string CreatedUserName { get; set; }
      
      public string CreatedUserRole { get; set; }
      
      public int TeamId { get; set; }
      
      public string TeamName { get; set; }
      
      public DateTime DueDate { get; set; }
      
      public string Status { get; set; }
}