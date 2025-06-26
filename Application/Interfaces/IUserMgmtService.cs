using Domain.Entities;

namespace Application.Interfaces;

public interface IUserMgmtService
{
    Task<IEnumerable<User>> GetUsersAsync();
    Task<User?> GetUserByIdAsync(int id);
    Task<User> CreateUserAsync(User user);
    Task<User?> ValidateUserCredentialsAsync(string email, string password);
    Task<User> UpdateUserAsync(User user);
    Task<User?> DeleteUserAsync(int id);
}