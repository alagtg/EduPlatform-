using System.ComponentModel.DataAnnotations;

namespace EduPlatform.API.DTOs.Comments
{
    public class CreateCommentRequest
    {
        [MaxLength(80)] public string? UserName { get; set; }
        [Required, MaxLength(1000)] public string Message { get; set; } = default!;
    }
}
