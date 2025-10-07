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
            _fileService = fileService; _http = http;
        }

        private int CurrentProfId => int.Parse(User.FindFirstValue("profId")!);

        [HttpPost("upload")]
        [RequestSizeLimit(104857600)] // 100MB
        public async Task<IActionResult> Upload([FromForm] FileUploadRequest request)
        {
            var res = await _fileService.UploadAsync(CurrentProfId, request);
            return Ok(res);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _fileService.DeleteAsync(id, CurrentProfId);
            return NoContent();
        }
    }
}
