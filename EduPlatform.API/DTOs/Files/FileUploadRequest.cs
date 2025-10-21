using Microsoft.AspNetCore.Http;
using EduPlatform.API.Models;

namespace EduPlatform.API.DTOs.Files
{
    public class FileUploadRequest
    {
        public string Title { get; set; } = string.Empty;
        public FileType Type { get; set; }
        public int ClassId { get; set; } // ✅ corriger ici
        public IFormFile File { get; set; } = null!;
    }
}
