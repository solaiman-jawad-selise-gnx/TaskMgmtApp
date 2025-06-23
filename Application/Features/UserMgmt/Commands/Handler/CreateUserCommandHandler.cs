using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using FluentValidation;
using MediatR;

namespace Application.Features.UserMgmt.Commands.Handler;

public class CreateUserCommandHandler: IRequestHandler<CreateUserCommand, User>
{
    private readonly IUserMgmtService _userMgmtService;
    private readonly IValidator<CreateUserCommand> _validator;
    
    public CreateUserCommandHandler(IUserMgmtService userMgmtService, IValidator<CreateUserCommand> validator)
    {
        _userMgmtService = userMgmtService;
        _validator = validator;
    }
    
    public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        // Validate the request
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        // Parse the role from the request
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