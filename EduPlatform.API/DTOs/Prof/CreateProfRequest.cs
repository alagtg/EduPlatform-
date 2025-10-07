using System.ComponentModel.DataAnnotations;

namespace EduPlatform.API.DTOs.Prof
{
    public class CreateProfRequest
    {
        [Required, MaxLength(120)] public string Name { get; set; } = default!;
        [Required, EmailAddress] public string Email { get; set; } = default!;
        [Required, MinLength(6)] public string Password { get; set; } = default!;
        [Required, MaxLength(160)] public string Slug { get; set; } = default!;
    }
}
