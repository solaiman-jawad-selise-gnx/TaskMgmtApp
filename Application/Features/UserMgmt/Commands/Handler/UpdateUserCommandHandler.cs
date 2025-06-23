using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Features.UserMgmt.Commands.Handler;

public class UpdateUserCommandHandler: IRequestHandler<UpdateUserCommand, User>
{
    private readonly IUserMgmtService _userMgmtService;

    public UpdateUserCommandHandler(IUserMgmtService userMgmtService)
    {
        _userMgmtService = userMgmtService;
    }

    public async Task<User> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        bool enumParsed = Enum.TryParse<Role>(request.Role,true , out Role role);
        role = enumParsed ? role : Role.Employee; // Default to Employee if parsing fails
        var user = new User{
            Id = request.Id,
            FullName = request.FullName,
            Email = request.Email,
            Role = role,
        };
        return await _userMgmtService.UpdateUserAsync(user);
    }

}