using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduPlatform.API.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Required]
        public int FileResourceId { get; set; }

        [ForeignKey(nameof(FileResourceId))]
        public FileResource File { get; set; } = default!;

        [MaxLength(80)]
        public string? UserName { get; set; }

        [Required, MaxLength(1000)]
        public string Message { get; set; } = default!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
