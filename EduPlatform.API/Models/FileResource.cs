namespace EduPlatform.API.Models
{
    public enum FileType
    {
        Cours = 0,
        TD = 1,
        TP = 2,
        Autre = 3
    }

    public class FileResource
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public FileType Type { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int ProfId { get; set; }
        public Prof Prof { get; set; } = null!;
        public int? ClassroomId { get; set; }
        public Classroom? Classroom { get; set; }
        public int DownloadCount { get; set; } = 0;
    }
}
