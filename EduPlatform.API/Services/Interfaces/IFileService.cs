using EduPlatform.API.DTOs.Files;

namespace EduPlatform.API.Services.Interfaces
{
    public interface IFileService
    {
        Task<FileResponse> UploadAsync(int profId, FileUploadRequest request);
        Task<List<FileResponse>> ListByProfSlugAsync(string slug, string baseUrl);
        Task<(Stream stream, string fileName, string contentType)?> GetForDownloadAsync(int id);
        Task DeleteAsync(int id, int profId);
    }
}
