using Application.Interfaces;
using Application.Services;
using Domain.Entities;
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
        var user = new User{
            Id = request.Id,
            FullName = request.FullName,
            Email = request.Email,
            Role = request.Role,
        };
        return await _userMgmtService.UpdateUserAsync(user);
    }

}