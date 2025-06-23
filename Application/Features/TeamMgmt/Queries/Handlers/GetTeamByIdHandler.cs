using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.TeamMgmt.Queries.Handlers;

public class GetTeamByIdHandler: IRequestHandler<GetTeamByIdQuery, Team?>
{
    private readonly ITeamMgmtService _service;
    private readonly IValidator<GetTeamByIdQuery> _validator;

    public GetTeamByIdHandler(ITeamMgmtService service, IValidator<GetTeamByIdQuery> validator)
    {
        _validator = validator;
        _service = service;
    }
    
    public async Task<Team?> Handle(GetTeamByIdQuery request, CancellationToken cancellationToken)
    {
        // Validate the request
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        // Retrieve the team by ID
        return await _service.GetTeamByIdAsync(request.TeamId);
    }
    
    
}