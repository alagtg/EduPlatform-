using System.Security.Claims;
using EduPlatform.API.DTOs.Files;
using EduPlatform.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduPlatform.API.Controllers
{
    [ApiController]
    [Route("api/files")]
    [Authorize]
    public class FilesController : ControllerBase
    {
        private readonly IFileService _fileService;
        private readonly IHttpContextAccessor _http;

        public FilesController(IFileService fileService, IHttpContextAccessor http)
        {
            _fileService = fileService;
            _http = http;
        }

        private int CurrentProfId => int.Parse(User.FindFirstValue("profId")!);

        // =============================
        // ✅ 1️⃣ Upload de fichier
        // =============================
        [HttpPost("upload")]
        [RequestSizeLimit(104857600)] // 100MB max
        public async Task<IActionResult> Upload([FromForm] FileUploadRequest request)
        {
            var res = await _fileService.UploadAsync(CurrentProfId, request);
            return Ok(res);
        }

        // =============================
        // ✅ 2️⃣ Supprimer un fichier
        // =============================
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _fileService.DeleteAsync(id, CurrentProfId);
            return NoContent();
        }

        // =============================
        // ✅ 3️⃣ Lister les fichiers d’une classe
        // =============================
        [AllowAnonymous]
        [HttpGet("by-class/{classId:int}")]
        public async Task<IActionResult> GetByClass(int classId)
        {
            var baseUrl = $"{Request.Scheme}://{Request.Host}";
            var files = await _fileService.ListByClassAsync(classId, baseUrl);
            return Ok(files);
        }

        // =============================
        // ✅ 4️⃣ Lister les fichiers du prof connecté
        // =============================
        [HttpGet("my-files")]
        public async Task<IActionResult> GetMyFiles()
        {
            var slug = User.FindFirstValue("slug");
            var baseUrl = $"{Request.Scheme}://{Request.Host}";
            var files = await _fileService.ListByProfSlugAsync(slug!, baseUrl);
            return Ok(files);
        }

        // =============================
        // ✅ 5️⃣ Télécharger un fichier
        // =============================
        [AllowAnonymous]
        [HttpGet("download/{id:int}")]
        public async Task<IActionResult> Download(int id)
        {
            var file = await _fileService.GetForDownloadAsync(id);
            if (file == null) return NotFound();

            var (stream, fileName, contentType) = file.Value;
            return File(stream, contentType, fileName);
        }
    }
}
