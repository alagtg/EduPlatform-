using EduPlatform.API.Models;

namespace EduPlatform.API.Repositories.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetByFileIdAsync(int fileId);
        Task AddAsync(Comment comment);
        Task SaveAsync();
    }
}
