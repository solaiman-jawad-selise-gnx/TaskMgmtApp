using Application.Interfaces;
using Application.RepositoryInterfaces;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class UserMgmtService : IUserMgmtService
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<UserMgmtService> _logger;
    
    public UserMgmtService(IUserRepository userRepository, ILogger<UserMgmtService> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }
    
    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        _logger.LogInformation("Fetching all users");
        return await _userRepository.GetAllUsersAsync();
    }
    
    public async Task<User?> GetUserByIdAsync(int id)
    {
        _logger.LogInformation("Fetching user with ID: {Id}", id);
        return await _userRepository.GetUserByIdAsync(id);
    }
    
    public async Task<User> CreateUserAsync(User user)
    {
        _logger.LogInformation("Creating user: {@User}", user);
        return await _userRepository.AddUserAsync(user);
    }
    
    public async Task<User> UpdateUserAsync(User user)
    {
        _logger.LogInformation("Updating user: {@User}", user);
        return await _userRepository.UpdateUserAsync(user);
    }
    
    public async Task<User?> DeleteUserAsync(int id)
    {
        _logger.LogInformation("Deleting user with ID: {Id}", id);
       return await _userRepository.DeleteUserAsync(id);
    }
}
    