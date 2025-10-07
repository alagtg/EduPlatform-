using EduPlatform.API.DTOs.Prof;

namespace EduPlatform.API.Services.Interfaces
{
    public interface IProfService
    {
        Task<ProfResponse> CreateAsync(CreateProfRequest request);
        Task<ProfResponse?> GetByIdAsync(int id);
        Task<List<ProfResponse>> GetAllAsync();

        Task<ProfResponse?> GetBySlugAsync(string slug);
    }
}
