namespace EduPlatform.API.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int FileResourceId { get; set; }
        public FileResource File { get; set; } = null!;
        public string UserName { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
