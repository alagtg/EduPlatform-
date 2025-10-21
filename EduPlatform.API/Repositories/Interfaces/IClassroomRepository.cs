using EduPlatform.API.Models;

namespace EduPlatform.API.Repositories.Interfaces
{
    public interface IClassroomRepository
    {
        Task AddAsync(Classroom c);
        Task<IEnumerable<Classroom>> GetByProfIdAsync(int profId);
        Task<Classroom?> GetByCodeAsync(string code);
        Task SaveAsync();
    }
}
