using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduPlatform.API.Models
{
    public class CahierPedagogique
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ClassId { get; set; }

        [ForeignKey("ClassId")]
        public virtual Classroom? Classroom { get; set; } // nullable

        [Required]
        [MaxLength(255)]
        public string FileName { get; set; } = string.Empty;  // ✅

        [Required]
        public string FilePath { get; set; } = string.Empty;  // ✅

        [Required]
        [MaxLength(255)]
        public string FileUrl { get; set; } = string.Empty;   // ✅

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
