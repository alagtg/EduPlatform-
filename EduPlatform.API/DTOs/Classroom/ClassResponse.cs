namespace EduPlatform.API.DTOs.Classroom
{
    public class ClassResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string AccessCode { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
