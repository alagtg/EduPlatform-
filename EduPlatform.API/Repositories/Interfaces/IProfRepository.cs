using EduPlatform.API.Models;

namespace EduPlatform.API.Repositories.Interfaces
{
    public interface IProfRepository
    {
        Task<Prof?> GetByEmailAsync(string email);
        Task<Prof?> GetByIdAsync(int id);
        Task<Prof?> GetBySlugAsync(string slug);
        Task AddAsync(Prof prof);
        Task<List<Prof>> GetAllAsync();

        Task<bool> EmailExistsAsync(string email);
        Task<bool> SlugExistsAsync(string slug);
        Task SaveAsync();
    }
}
