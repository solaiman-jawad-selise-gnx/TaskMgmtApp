using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using MediatR;

namespace Application.Features.Login.Commands.UserLogin.Handler;

public class LoginCommandHandler: IRequestHandler<LoginCommand, string>
{
    private readonly ILoginService _loginService;

    public LoginCommandHandler(ILoginService loginService)
    {
        _loginService = loginService;
    }
    public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var jwtToken = await _loginService.LoginAsync(request.Email, request.Password);
        return jwtToken;
    }
    
}