using EduPlatform.API.DTOs.Classroom;

namespace EduPlatform.API.Services.Interfaces
{
    public interface IClassroomService
    {
        Task<ClassResponse> CreateAsync(int profId, CreateClassRequest request);
        Task<IEnumerable<ClassResponse>> GetByProfIdAsync(int profId);
        Task<ClassResponse?> GetByCodeAsync(string code);
    }
}
