using Application.Features.TeamMgmt.Commands;
using Application.Features.TeamMgmt.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public TeamController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // GET api/<TeamController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Team>> GetTeamById(int id)
        {
            return await _mediator.Send(new GetTeamByIdQuery{TeamId = id});
        }

        // POST api/<TeamController>
        [HttpPost]
        public async Task<ActionResult<Team>> CreateTeam([FromBody] CreateTeamCommand command)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetTeamById), new { id = result.Id }, result);
        }

        // PUT api/<TeamController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Team>> UpdateTeam(int id, [FromBody] UpdateTeamCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("Team ID mismatch");
            }
            return await _mediator.Send(command);
        }

        // DELETE api/<TeamController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            var command = new DeleteTeamCommand { Id = id };
            var result = await _mediator.Send(command);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound($"Team with ID {id} not found.");
        }
    }
}
