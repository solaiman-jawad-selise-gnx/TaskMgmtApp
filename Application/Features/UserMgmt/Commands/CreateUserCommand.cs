using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Features.UserMgmt.Commands;

public class CreateUserCommand: IRequest<User>
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    
    public string Password { get; set; }
}