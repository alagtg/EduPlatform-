namespace EduPlatform.API.Models
{
    public class Classroom
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string? Description { get; set; }
        public string AccessCode { get; set; } = "";
        public int ProfId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Prof Prof { get; set; } = null!;
        public virtual ICollection<CahierPedagogique>? Cahiers { get; set; }
        public List<FileResource> Files { get; set; } = new();

    }
}
