using Application.Features.TaskMgmt.Commands;
using Application.Features.TaskMgmt.Queries;
using Application.QueryParams;
using AutoMapper;
using Domain.Entities;
using Infrastructure.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.MapperConfig;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        
        public TaskController(IMediator mediator)
        {
            _mediator = mediator;
            _mapper = TaskObjMapper.InitializeAutomapper();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetTaskDto>>> GetTasks([FromQuery] TaskQueryParameters queryParameters)
        {
            GetTasksQuery query = new GetTasksQuery
            {
                parameters = queryParameters
            };
            
            var tasks = await _mediator.Send(query);
            
            return Ok(tasks.Select(task => _mapper.Map<TaskItem, GetTaskDto>(task)));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskItem>> GetTaskById(int id)
        {
            var result = await _mediator.Send(new GetTaskByIdQuery { TaskId = id });
            if (result == null) return NotFound($"Task with ID {id} not found.");
            return Ok(_mapper.Map<TaskItem, GetTaskDto>(result));
        }

        [HttpPost]
        public async Task<ActionResult<TaskItem>> CreateTask([FromBody] CreateTaskDto body)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var command = _mapper.Map<CreateTaskDto, CreateTaskCommand>(body);
            
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetTaskById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TaskItem>> UpdateTask(int id, [FromBody] UpdateTaskDto body)
        {
            if (id != body.taskId)
            {
                return BadRequest("Task ID mismatch");
            }
            var command = _mapper.Map<UpdateTaskDto, UpdateTaskCommand>(body);
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
