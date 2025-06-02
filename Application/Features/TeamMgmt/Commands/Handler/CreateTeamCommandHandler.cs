using Application.Interfaces;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.TeamMgmt.Commands.Handler;

public class CreateTeamCommandHandler: IRequestHandler<CreateTeamCommand, Team>
{
    private readonly ITeamMgmtService _teamMgmtService;
    private readonly IValidator<CreateTeamCommand> _validator;
    
    public CreateTeamCommandHandler(ITeamMgmtService teamMgmtService, IValidator<CreateTeamCommand> validator)
    {
        _teamMgmtService = teamMgmtService;
        _validator = validator;
    }
    
    public async Task<Team> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
    {
        // Validate the request
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        var team = new Team
        {
            Name = request.Name,
            Description = request.Description,
        };

        return await _teamMgmtService.CreateTeamAsync(team);
    }
}