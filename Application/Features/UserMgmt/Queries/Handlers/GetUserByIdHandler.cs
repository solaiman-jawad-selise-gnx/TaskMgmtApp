using Application.Interfaces;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.UserMgmt.Queries.Handlers;

public class GetUserByIdHandler: IRequestHandler<GetUserByIdQuery, User?>
{
    private readonly IUserMgmtService _userMgmtService;
    private readonly IValidator<GetUserByIdQuery> _validator;

    public GetUserByIdHandler(IUserMgmtService userMgmtService, IValidator<GetUserByIdQuery> validator)
    {
        _userMgmtService = userMgmtService;
        _validator = validator;
    }

    public async Task<User?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        // Validate the request
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        // Retrieve the user by ID
        return await _userMgmtService.GetUserByIdAsync(request.UserId);
    }
    
}