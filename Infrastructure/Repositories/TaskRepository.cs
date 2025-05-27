using Application.RepositoryInterfaces;
using Domain.Entities;
using Infrastructure.DB;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;


public class TaskRepository : ITaskRepository
{
    private readonly AppDbContext _context;

    public TaskRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<TaskItem?> GetTaskByIdAsync(int taskId)
    { 
        return await _context.Tasks
            .Include(t => t.AssignedToUser)
            .Include(t => t.CreatedByUser)
            .Include(t => t.Team)
            .FirstOrDefaultAsync(t => t.Id == taskId);
    }

    public async Task<IEnumerable<TaskItem>> GetAllTasksAsync()
    {
        return await _context.Tasks.ToListAsync();
    }
    public async Task<TaskItem> CreateTaskAsync(TaskItem task)
    {
        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();
        return task;
    }
    
    public async Task<TaskItem> UpdateTaskAsync(TaskItem task)
    {
        _context.Tasks.Update(task);
        await _context.SaveChangesAsync();
        return task;
    }
public async Task<TaskItem?> UpdateTaskStatusAsync(int taskId, Domain.Enums.TaskMgmtStatus status)
    {
        var task = await _context.Tasks.FindAsync(taskId);
        if (task != null)
        {
            task.Status = status;
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
        }
        return task;
    }
    public async Task<TaskItem?> DeleteTaskAsync(int taskId)
    {
        var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == taskId);;
        if (task != null)
        {
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
        }
        return task;
    }
}

