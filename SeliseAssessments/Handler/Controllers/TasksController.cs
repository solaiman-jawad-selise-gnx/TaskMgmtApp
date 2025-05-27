using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SeliseAssessments.Model;

namespace SeliseAssessments.Handler.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly AppDbContext _context;

    private readonly IMapper _mapper;

    public TasksController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskItemDto>>> GetTasks([FromQuery] TaskQueryParameters query)
    {
        var tasks = _context.Tasks
            .Include(t => t.AssignedToUser)
            .Include(t => t.CreatedByUser)
            .Include(t => t.Team)
            .AsQueryable();

        // Filtering logic...
        if (query.Status.HasValue)
            tasks = tasks.Where(t => t.Status == query.Status);

        tasks = tasks
            .OrderBy(t => t.DueDate)
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize);

        var result = await tasks.ToListAsync();
        return Ok(_mapper.Map<List<TaskItemDto>>(result));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TaskItemDto>> GetTask(int id)
    {
        var task = await _context.Tasks
            .Include(t => t.AssignedToUser)
            .Include(t => t.CreatedByUser)
            .Include(t => t.Team)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (task == null) return NotFound();
        return Ok(_mapper.Map<TaskItemDto>(task));
    }

    [HttpPost]
    public async Task<ActionResult<TaskItemDto>> CreateTask(TaskItemCreateDto dto)
    {
        var task = _mapper.Map<TaskItem>(dto);
        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();

        task = await _context.Tasks
            .Include(t => t.AssignedToUser)
            .Include(t => t.CreatedByUser)
            .Include(t => t.Team)
            .FirstOrDefaultAsync(t => t.Id == task.Id);

        return CreatedAtAction(nameof(GetTask), new { id = task.Id }, _mapper.Map<TaskItemDto>(task));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTask(int id, TaskItemUpdateDto dto)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task == null) return NotFound();

        _mapper.Map(dto, task);
        await _context.SaveChangesAsync();
        return NoContent();
    }
    [HttpPatch("{id}/status")]
    public async Task<IActionResult> UpdateTaskStatus(int id, [FromBody] MgmtTaskStatus status)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task == null) return NotFound();

        // Assume currentUserId comes from auth context
        var currentUserId = 3; // mock for Employee
        if (task.AssignedToUserId != currentUserId)
            return Forbid("You can only update your own tasks.");

        task.Status = status;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task == null) return NotFound();

        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
