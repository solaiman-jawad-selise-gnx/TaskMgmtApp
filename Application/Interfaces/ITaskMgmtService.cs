using Application.QueryParams;
using Domain.Entities;
using Domain.Enums;

namespace Application.Interfaces;

public interface ITaskMgmtService
{
    Task<IEnumerable<TaskItem>> GetTasksAsync(TaskQueryParameters query);
    Task<TaskItem?> GetTaskByIdAsync(int id);
    Task<TaskItem> CreateTaskAsync(TaskItem task);
    Task<TaskItem> UpdateTaskAsync(TaskItem task);
    Task<TaskItem?> UpdateTaskStatusAsync(int id, TaskMgmtStatus status);
    Task<TaskItem?> DeleteTaskAsync(int id);
}