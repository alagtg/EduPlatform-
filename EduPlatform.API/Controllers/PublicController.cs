using EduPlatform.API.DTOs.Comments;
using EduPlatform.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EduPlatform.API.Controllers
{
    [ApiController]
    [Route("api/public")]
    public class PublicController : ControllerBase
    {
        private readonly IFileService _files;
        private readonly Repositories.Interfaces.ICommentRepository _comments;

        public PublicController(IFileService files, Repositories.Interfaces.ICommentRepository comments)
        {
            _files = files; _comments = comments;
        }

        [HttpGet("files/{profSlug}")]
        public async Task<IActionResult> List(string profSlug)
        {
            var baseUrl = $"{Request.Scheme}://{Request.Host}";
            var list = await _files.ListByProfSlugAsync(profSlug, baseUrl);
            return Ok(list);
        }

        [HttpGet("download/{id:int}")]
        public async Task<IActionResult> Download(int id)
        {
            var item = await _files.GetForDownloadAsync(id);
            if (item == null) return NotFound();

            var (stream, fileName, contentType) = item.Value;
            return File(stream, contentType, fileName);
        }

        [HttpGet("comments/{fileId:int}")]
        public async Task<IActionResult> GetComments(int fileId)
        {
            var list = await _comments.GetByFileIdAsync(fileId);
            var resp = list.Select(c => new CommentResponse
            {
                Id = c.Id, UserName = c.UserName, Message = c.Message, CreatedAt = c.CreatedAt
            });
            return Ok(resp);
        }

        [HttpPost("comments/{fileId:int}")]
        public async Task<IActionResult> AddComment(int fileId, [FromBody] CreateCommentRequest req)
        {
            var entity = new Models.Comment
            {
                FileResourceId = fileId,
                UserName = req.UserName,
                Message = req.Message
            };
            await _comments.AddAsync(entity);
            await _comments.SaveAsync();
            return Ok(new CommentResponse { Id = entity.Id, UserName = entity.UserName, Message = entity.Message, CreatedAt = entity.CreatedAt });
        }
    }
}
