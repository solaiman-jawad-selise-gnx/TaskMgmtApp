using Application.Features.UserMgmt.Commands;
using Application.Features.UserMgmt.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var result = await _mediator.Send(new GetUserByIdQuery{UserId = id});
            if (result == null) return NotFound();
            return Ok(result);
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetUserById), new { id = result.Id }, result);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserCommand command)
        {
            if (id != command.Id) return BadRequest("User ID mismatch.");
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _mediator.Send(new DeleteUserCommand{Id = id});
            if (result == null) return NotFound();
            return NoContent();
        }
    }
}
