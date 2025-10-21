namespace EduPlatform.API.DTOs.Classroom
{
    public class CreateClassRequest
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
