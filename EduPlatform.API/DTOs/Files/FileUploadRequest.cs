using System.ComponentModel.DataAnnotations;
using EduPlatform.API.Models;
using Microsoft.AspNetCore.Http;

namespace EduPlatform.API.DTOs.Files
{
    public class FileUploadRequest
    {
        [Required] public string Title { get; set; } = default!;
        [Required] public FileType Type { get; set; }
        [Required] public IFormFile File { get; set; } = default!;
    }
}
