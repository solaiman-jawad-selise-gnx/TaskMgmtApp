using Domain.Entities;
using Domain.Enums;

namespace Application.RepositoryInterfaces;

public interface ITaskRepository
{
    Task<TaskItem?> GetTaskByIdAsync(int taskId);
    Task<IEnumerable<TaskItem>> GetAllTasksAsync();
    Task<TaskItem> CreateTaskAsync(TaskItem task);
    Task<TaskItem> UpdateTaskAsync(TaskItem task);
    Task<TaskItem?> UpdateTaskStatusAsync(int taskId, TaskMgmtStatus status);
    Task<TaskItem?> DeleteTaskAsync(int taskId);
}