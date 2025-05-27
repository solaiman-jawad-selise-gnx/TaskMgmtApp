using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using MediatR;

namespace Application.Features.UserMgmt.Commands.Handler;

public class CreateUserCommandHandler: IRequestHandler<CreateUserCommand, User>
{
    private readonly IUserMgmtService _userMgmtService;
    
    public CreateUserCommandHandler(IUserMgmtService userMgmtService)
    {
        _userMgmtService = userMgmtService;
    }
    
    public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User
        {
            FullName = request.FullName,
            Email = request.Email,
            Role = request.Role,
        };

        return await _userMgmtService.CreateUserAsync(user);
    }
}