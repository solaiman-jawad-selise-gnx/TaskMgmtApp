using Application.Features.TaskMgmt.Commands;
using Application.Features.TaskMgmt.Queries;
using Application.QueryParams;
using Asp.Versioning;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
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
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<TaskItem>> GetTaskById(int id)
        {
            var result = await _mediator.Send(new GetTaskByIdQuery { TaskId = id });
            if (result == null) return NotFound($"Task with ID {id} not found.");
            return Ok(result);
        }

        [HttpGet("{id}")]
        [MapToApiVersion("2.0")]
        public async Task<ActionResult<TaskItem>> GetTaskByIdV2(int id)
        {
            var result = await _mediator.Send(new GetTaskByIdQuery { TaskId = id });
            if (result == null) return NotFound($"Task with ID {id} not found.");
            return Ok(result);
        }
        
        [HttpPost]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<TaskItem>> CreateTask([FromBody] CreateTaskCommand command)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetTaskById), new { id = result.Id }, result);
        }

        [HttpPost]
        [MapToApiVersion("2.0")]
        public async Task<ActionResult<TaskItem>> CreateTaskV2([FromBody] CreateTaskCommand command)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            var result = await _mediator.Send(command);
            return Ok("Your task has been created successfully using create task V2");
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
