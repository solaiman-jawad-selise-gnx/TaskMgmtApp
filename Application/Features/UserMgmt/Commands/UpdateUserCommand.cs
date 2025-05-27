using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Features.UserMgmt.Commands;

public class UpdateUserCommand: IRequest<User>
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
}