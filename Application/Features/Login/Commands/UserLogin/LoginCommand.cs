using Domain.Entities;
using MediatR;

namespace Application.Features.Login.Commands.UserLogin;

public class LoginCommand: IRequest<string>
{
    public string Email { get; set; }
    public string Password { get; set; }
}