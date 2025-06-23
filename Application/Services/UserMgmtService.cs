using Application.Interfaces;
using Application.RepositoryInterfaces;
using Domain.Entities;

namespace Application.Services;

public class UserMgmtService : IUserMgmtService
{
    private readonly IUserRepository _userRepository;
    
    public UserMgmtService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        return await _userRepository.GetAllUsersAsync();
    }
    
    public async Task<User?> GetUserByIdAsync(int id)
    {
        return await _userRepository.GetUserByIdAsync(id);
    }
    
    public async Task<User> CreateUserAsync(User user)
    {
        return await _userRepository.AddUserAsync(user);
    }

    public Task<User?> ValidateUserCredentialsAsync(string email, string password)
    {
        var user = _userRepository.GetUserByEmailAndPasswordAsync(email, password);
        return user;
    }

    public async Task<User> UpdateUserAsync(User user)
    {
        return await _userRepository.UpdateUserAsync(user);
    }
    
    public async Task<User?> DeleteUserAsync(int id)
    {
       return await _userRepository.DeleteUserAsync(id);
    }
}
    