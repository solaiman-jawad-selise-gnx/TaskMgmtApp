using Domain.Entities;
using MediatR;

namespace Application.Features.UserMgmt.Commands;

public class DeleteUserCommand: IRequest<User?>
{
    public int Id { get; set; }
}