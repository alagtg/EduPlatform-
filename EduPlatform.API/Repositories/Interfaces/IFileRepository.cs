using EduPlatform.API.Models;

namespace EduPlatform.API.Repositories.Interfaces
{
    public interface IFileRepository
    {
        Task<FileResource?> GetByIdAsync(int id);
        Task<List<FileResource>> GetByProfSlugAsync(string slug);
        Task AddAsync(FileResource file);
        Task DeleteAsync(FileResource file);
        Task SaveAsync();
    }
}
