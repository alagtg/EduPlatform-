using EduPlatform.API.DTOs.Auth;

namespace EduPlatform.API.Services.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponse?> LoginAsync(LoginRequest request);
        string HashPassword(string password);
        bool VerifyPassword(string password, string passwordHash);
    }
}
