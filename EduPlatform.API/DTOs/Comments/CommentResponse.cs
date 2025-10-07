namespace EduPlatform.API.DTOs.Comments
{
    public class CommentResponse
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string Message { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
    }
}
