using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduPlatform.API.Models
{
    public class FileResource
    {
        public int Id { get; set; }

        [Required]
        public int ProfId { get; set; }

        [ForeignKey(nameof(ProfId))]
        public Prof Prof { get; set; } = default!;

        [Required, MaxLength(180)]
        public string Title { get; set; } = default!;

        [Required]
        public FileType Type { get; set; }

        [Required, MaxLength(300)]
        public string Path { get; set; } = default!; // relative: /Uploads/xxx.pdf

        public int DownloadCount { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
