using Domain.Entities;

namespace Application.Interfaces;

public interface ITeamMgmtService
{
    Task<IEnumerable<Team>> GetTeamsAsync();
    Task<Team?> GetTeamByIdAsync(int id);
    Task<Team> CreateTeamAsync(Team team);
    Task<Team> UpdateTeamAsync(Team team);
    Task<Team?> DeleteTeamAsync(int id);
}