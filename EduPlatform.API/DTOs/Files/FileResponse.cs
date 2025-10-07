using EduPlatform.API.Models;

namespace EduPlatform.API.DTOs.Files
{
    public class FileResponse
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public FileType Type { get; set; }
        public string Url { get; set; } = default!;
        public int DownloadCount { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ProfSlug { get; set; } = default!;
    }
}
