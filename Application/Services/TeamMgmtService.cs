using Application.Interfaces;
using Application.RepositoryInterfaces;
using Domain.Entities;

namespace Application.Services;

public class TeamMgmtService : ITeamMgmtService
{
    private readonly ITeamRepository _teamRepository;
    
    public TeamMgmtService(ITeamRepository teamRepository)
    {
        _teamRepository = teamRepository;
    }
    
    public async Task<IEnumerable<Team>> GetTeamsAsync()
    {
        return await _teamRepository.GetAllTeamsAsync();
    }
    
    public async Task<Team?> GetTeamByIdAsync(int id)
    {
        return await _teamRepository.GetTeamByIdAsync(id);
    }
    
    public async Task<Team> CreateTeamAsync(Team team)
    {
        return await _teamRepository.AddTeamAsync(team);
    }
    public async Task<Team> UpdateTeamAsync(Team team)
    {
        return await _teamRepository.UpdateTeamAsync(team);
    }
    
    public async Task<Team?> DeleteTeamAsync(int id)
    {
        return await _teamRepository.DeleteTeamAsync(id);
    }
    
}