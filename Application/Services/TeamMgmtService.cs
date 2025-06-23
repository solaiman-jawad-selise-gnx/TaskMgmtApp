using Application.Interfaces;
using Application.RepositoryInterfaces;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class TeamMgmtService : ITeamMgmtService
{
    private readonly ITeamRepository _teamRepository;
    private readonly ILogger<TeamMgmtService> _logger;
    
    public TeamMgmtService(ITeamRepository teamRepository, ILogger<TeamMgmtService> logger)
    {
        _teamRepository = teamRepository;
        _logger = logger;
        
    }
    
    public async Task<IEnumerable<Team>> GetTeamsAsync()
    {
        _logger.LogInformation("Fetching all teams");
        return await _teamRepository.GetAllTeamsAsync();
    }
    
    public async Task<Team?> GetTeamByIdAsync(int id)
    {
        _logger.LogInformation("Fetching team with ID: {Id}", id);
        return await _teamRepository.GetTeamByIdAsync(id);
    }
    
    public async Task<Team> CreateTeamAsync(Team team)
    {
        _logger.LogInformation("Creating team: {@Team}", team);
        return await _teamRepository.AddTeamAsync(team);
    }
    public async Task<Team> UpdateTeamAsync(Team team)
    {
        _logger.LogInformation("Updating team: {@Team}", team);
        return await _teamRepository.UpdateTeamAsync(team);
    }
    
    public async Task<Team?> DeleteTeamAsync(int id)
    {
        _logger.LogInformation("Deleting team with ID: {Id}", id);
        return await _teamRepository.DeleteTeamAsync(id);
    }
    
}