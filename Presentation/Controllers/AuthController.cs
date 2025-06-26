using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Features.Login.Commands.UserLogin;
using Domain.Entities;
using MediatR;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IMediator _mediator;

        public AuthController(IConfiguration configuration, IMediator mediator)
        {
            _configuration = configuration;
            _mediator = mediator;
        }
        
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] LoginCommand loginCommand)
        {
            var token = await _mediator.Send(loginCommand);
            return Ok(token);
        }
    }
}
