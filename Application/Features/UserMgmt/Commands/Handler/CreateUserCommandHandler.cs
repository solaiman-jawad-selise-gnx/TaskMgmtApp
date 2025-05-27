using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Domain.Enums;
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
        bool enumParsed = Enum.TryParse<Role>(request.Role,true , out Role role);
        role = enumParsed ? role : Role.Employee; // Default to Employee if parsing fails
        var user = new User
        {
            FullName = request.FullName,
            Email = request.Email,
            Role = role,
        };

        return await _userMgmtService.CreateUserAsync(user);
    }
}