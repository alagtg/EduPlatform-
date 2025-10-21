namespace EduPlatform.API.DTOs.Files
{
    public class FileResponse
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public string FileUrl { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string? ClassroomName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
