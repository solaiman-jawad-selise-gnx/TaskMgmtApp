using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using MediatR;

namespace Application.Features.UserMgmt.Commands.Handler;

public class DeleteUserCommandHandler: IRequestHandler<DeleteUserCommand, User?>
{
    private readonly IUserMgmtService _userMgmtService;
    
    public DeleteUserCommandHandler(IUserMgmtService userMgmtService)
    {
        _userMgmtService = userMgmtService;
    }

    public async Task<User?> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var deletedUser = await _userMgmtService.DeleteUserAsync(request.Id);
        return deletedUser;
    }
}