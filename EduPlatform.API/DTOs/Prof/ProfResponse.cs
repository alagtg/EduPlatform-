namespace EduPlatform.API.DTOs.Prof
{
    public class ProfResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Slug { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
    }
}
