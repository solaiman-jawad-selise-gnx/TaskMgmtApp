using Domain.Entities;

namespace Application.RepositoryInterfaces;

public interface ITeamRepository
{
    Task<Team?> GetTeamByIdAsync(int id);
    Task<IEnumerable<Team>> GetAllTeamsAsync();
    Task<Team> AddTeamAsync(Team team);
    Task<Team> UpdateTeamAsync(Team team);
    Task<Team?> DeleteTeamAsync(int id);
}