using Application.Interfaces;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.UserMgmt.Commands.Handler;

public class DeleteUserCommandHandler: IRequestHandler<DeleteUserCommand, User?>
{
    private readonly IUserMgmtService _userMgmtService;
    private readonly IValidator<DeleteUserCommand> _validator;
    
    public DeleteUserCommandHandler(IUserMgmtService userMgmtService, IValidator<DeleteUserCommand> validator)
    {
        _userMgmtService = userMgmtService;
        _validator = validator;
    }

    public async Task<User?> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        // Validate the request
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        // Delete the user
        var deletedUser = await _userMgmtService.DeleteUserAsync(request.Id);
        return deletedUser;
    }
}