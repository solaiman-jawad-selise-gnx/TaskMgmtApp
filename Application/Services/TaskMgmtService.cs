using Application.Interfaces;
using Application.QueryParams;
using Application.RepositoryInterfaces;
using Domain.Entities;
using Domain.Enums;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class TaskMgmtService : ITaskMgmtService
{
    private readonly ITaskRepository _taskRepository;
    
    private readonly ILogger<TaskMgmtService> _logger;

    public TaskMgmtService(ITaskRepository taskRepository, ILogger<TaskMgmtService> logger)
    {
        _taskRepository = taskRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<TaskItem>> GetTasksAsync(TaskQueryParameters query)
    {
        _logger.LogInformation("Fetching tasks with query parameters: {@Query}", query);
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
        _logger.LogInformation("Fetching task with ID: {Id}", id);
        return await _taskRepository.GetTaskByIdAsync(id);
    }
    public async Task<TaskItem> CreateTaskAsync(TaskItem task)
    {
        _logger.LogInformation("Creating task: {@Task}", task);
        return await _taskRepository.CreateTaskAsync(task);
    }
    public async Task<TaskItem> UpdateTaskAsync(TaskItem task)
    {
        _logger.LogInformation("Updating task: {@Task}", task);
        return await _taskRepository.UpdateTaskAsync(task);
    }
    public async Task<TaskItem?> UpdateTaskStatusAsync(int id, TaskMgmtStatus status)
    {
        _logger.LogInformation("Updating status of task with ID {Id} to {Status}", id, status);
        return await _taskRepository.UpdateTaskStatusAsync(id, status);
    }
    
    public async Task<TaskItem?> DeleteTaskAsync(int id)
    {
        _logger.LogInformation("Deleting task with ID: {Id}", id);
        return await _taskRepository.DeleteTaskAsync(id);
    }
}