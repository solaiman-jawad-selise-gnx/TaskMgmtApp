using Domain.Entities;

namespace Application.RepositoryInterfaces;

public interface IUserRepository
{
    Task<User?> GetUserByIdAsync(int id);
    Task<User?> GetUserByEmailAsync(string email);
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<User> AddUserAsync(User user);
    Task<User> UpdateUserAsync(User user);
    Task<User?> DeleteUserAsync(int id);
}