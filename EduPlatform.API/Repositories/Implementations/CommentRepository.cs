using EduPlatform.API.Data;
using EduPlatform.API.Models;
using EduPlatform.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EduPlatform.API.Repositories.Implementations
{
    public class CommentRepository : ICommentRepository
    {
        private readonly EduDbContext _ctx;
        public CommentRepository(EduDbContext ctx) => _ctx = ctx;

        public Task<List<Comment>> GetByFileIdAsync(int fileId) =>
            _ctx.Comments.Where(c => c.FileResourceId == fileId)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();

        public async Task AddAsync(Comment comment) => await _ctx.Comments.AddAsync(comment);
        public Task SaveAsync() => _ctx.SaveChangesAsync();
    }
}
