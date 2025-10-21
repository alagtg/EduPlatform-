using EduPlatform.API.DTOs.Files;

namespace EduPlatform.API.Services.Interfaces
{
    public interface IFileService
    {
        Task<IEnumerable<FileResponse>> ListByProfSlugAsync(string slug, string baseUrl);
        Task<IEnumerable<FileResponse>> ListByClassAsync(int classId, string baseUrl);
        Task<FileResponse> UploadAsync(int profId, FileUploadRequest request);
        Task DeleteAsync(int id, int profId);
        Task<(Stream stream, string fileName, string contentType)?> GetForDownloadAsync(int id);


    }
}
