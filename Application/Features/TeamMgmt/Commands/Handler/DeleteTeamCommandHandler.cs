using Application.Interfaces;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.TeamMgmt.Commands.Handler;

public class DeleteTeamCommandHandler: IRequestHandler<DeleteTeamCommand, Team?>
{
    
    private readonly ITeamMgmtService _teamMgmtService;
    private readonly IValidator<DeleteTeamCommand> _validator;
    
    public DeleteTeamCommandHandler(ITeamMgmtService teamMgmtService, IValidator<DeleteTeamCommand> validator)
    {
        _teamMgmtService = teamMgmtService;
        _validator = validator;
    }

    public async Task<Team?> Handle(DeleteTeamCommand request, CancellationToken cancellationToken)
    {
        // Validate the request
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        // Delete the team
        var deletedTeam = await _teamMgmtService.DeleteTeamAsync(request.Id);
        return deletedTeam;
    }
}