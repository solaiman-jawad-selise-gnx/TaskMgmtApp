using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.Login.Commands.UserLogin.Handler;

public class LoginCommandHandler: IRequestHandler<LoginCommand, User?>
{
    private readonly IUserMgmtService _userMgmtService;

    public LoginCommandHandler(IUserMgmtService userMgmtService)
    {
        _userMgmtService = userMgmtService;
    }
    
    public async Task<User?> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        // Validate the user credentials
        var user = await _userMgmtService.ValidateUserCredentialsAsync(request.Email, request.Password);
        return user;
    }
    
}