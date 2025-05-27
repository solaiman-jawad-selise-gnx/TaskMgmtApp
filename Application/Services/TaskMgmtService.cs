using Application.Interfaces;
using Application.QueryParams;
using Application.RepositoryInterfaces;
using Domain.Entities;
using Domain.Enums;

namespace Application.Services;

public class TaskMgmtService : ITaskMgmtService
{
    private readonly ITaskRepository _taskRepository;

    public TaskMgmtService(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<IEnumerable<TaskItem>> GetTasksAsync(TaskQueryParameters query)
    {
        var tasks = await _taskRepository.GetAllTasksAsync();
        
        if (query.Status.HasValue)
            tasks = tasks.Where(t => t.Status == query.Status);
        
        if (query.AssignedToUserId.HasValue)
            tasks = tasks.Where(t => t.AssignedToUserId == query.AssignedToUserId);
        
        if (query.TeamId.HasValue)
            tasks = tasks.Where(t => t.TeamId == query.TeamId);
        
        if (query.DueDate.HasValue)
            tasks = tasks.Where(t => t.DueDate.Date <= query.DueDate.Value.Date);

        tasks = tasks
            .OrderBy(t => t.DueDate)
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize);

        return tasks;
    }
    public async Task<TaskItem?> GetTaskByIdAsync(int id)
    {
        return await _taskRepository.GetTaskByIdAsync(id);
    }
    public async Task<TaskItem> CreateTaskAsync(TaskItem task)
    {
        return await _taskRepository.CreateTaskAsync(task);
    }
    public async Task<TaskItem> UpdateTaskAsync(TaskItem task)
    {
        return await _taskRepository.UpdateTaskAsync(task);
    }
    public async Task<TaskItem?> UpdateTaskStatusAsync(int id, TaskMgmtStatus status)
    {
        return await _taskRepository.UpdateTaskStatusAsync(id, status);
    }
    
    public async Task<TaskItem?> DeleteTaskAsync(int id)
    {
        return await _taskRepository.DeleteTaskAsync(id);
    }
}