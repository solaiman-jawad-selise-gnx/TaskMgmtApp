using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using MediatR;

namespace Application.Features.UserMgmt.Queries.Handlers;

public class GetUserByIdHandler: IRequestHandler<GetUserByIdQuery, User?>
{
    private readonly IUserMgmtService _userMgmtService;

    public GetUserByIdHandler(IUserMgmtService userMgmtService)
    {
        _userMgmtService = userMgmtService;
    }

    public async Task<User?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        return await _userMgmtService.GetUserByIdAsync(request.UserId);
    }
    
}