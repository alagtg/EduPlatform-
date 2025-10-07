using System.ComponentModel.DataAnnotations;

namespace EduPlatform.API.Models
{
    public class Prof
    {
        public int Id { get; set; }

        [Required, MaxLength(120)]
        public string Name { get; set; } = default!;

        [Required, EmailAddress, MaxLength(200)]
        public string Email { get; set; } = default!;

        [Required]
        public string PasswordHash { get; set; } = default!;

        [Required, MaxLength(160)]
        public string Slug { get; set; } = default!; // ex: mme-houda

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<FileResource> Files { get; set; } = new List<FileResource>();
    }
}
