using Application.Features.TaskMgmt.Commands;
using Application.Features.TaskMgmt.Queries;
using Application.QueryParams;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public TaskController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItem>>> GetTasks([FromQuery] TaskQueryParameters queryParameters)
        {
            GetTasksQuery query = new GetTasksQuery
            {
                parameters = queryParameters
            };
            return Ok(await _mediator.Send(query));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskItem>> GetTaskById(int id)
        {
            var result = await _mediator.Send(new GetTaskByIdQuery { TaskId = id });
            if (result == null) return NotFound($"Task with ID {id} not found.");
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<TaskItem>> CreateTask([FromBody] CreateTaskCommand command)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetTaskById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TaskItem>> UpdateTask(int id, [FromBody] UpdateTaskCommand command)
        {
            if (id != command.taskId)
            {
                return BadRequest("Task ID mismatch");
            }
            return Ok(await _mediator.Send(command));
        }

        [HttpPatch("UpdateTaskStatus/{id}")]
        public async Task<ActionResult<TaskItem>> UpdateTaskStatus(int id, [FromBody] UpdateTaskStatusCommand command)
        {
            if (id != command.TaskId)
            {
                return BadRequest("Task ID mismatch");
            }
            return Ok(await _mediator.Send(command));
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var command = new DeleteTaskCommand { TaskId = id };
            var result = await _mediator.Send(command);
            if (result != null)
            {
                return NoContent();
            }
            return NotFound($"Task with ID {id} not found.");
        }
        
    }
}
