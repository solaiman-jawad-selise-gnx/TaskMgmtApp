using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using FluentValidation;
using MediatR;

namespace Application.Features.UserMgmt.Commands.Handler;

public class UpdateUserCommandHandler: IRequestHandler<UpdateUserCommand, User>
{
    private readonly IUserMgmtService _userMgmtService;
    private readonly IValidator<UpdateUserCommand> _validator;

    public UpdateUserCommandHandler(IUserMgmtService userMgmtService, IValidator<UpdateUserCommand> validator)
    {
        _userMgmtService = userMgmtService;
        _validator = validator;
    }

    public async Task<User> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
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
        var user = new User{
            Id = request.Id,
            FullName = request.FullName,
            Email = request.Email,
            Role = role,
        };
        return await _userMgmtService.UpdateUserAsync(user);
    }

}