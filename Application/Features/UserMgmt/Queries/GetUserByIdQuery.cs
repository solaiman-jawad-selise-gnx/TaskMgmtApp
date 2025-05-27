using Domain.Entities;
using MediatR;

namespace Application.Features.UserMgmt.Queries;

public class GetUserByIdQuery: IRequest<User?>
{
    public int UserId { get; set; }
    
}