namespace Application.Services;

public interface ILoginService
{
    Task<string> LoginAsync(string email, string password);
}