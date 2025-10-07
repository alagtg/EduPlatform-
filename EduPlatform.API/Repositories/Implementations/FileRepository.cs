using EduPlatform.API.Data;
using EduPlatform.API.Models;
using EduPlatform.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EduPlatform.API.Repositories.Implementations
{
    public class FileRepository : IFileRepository
    {
        private readonly EduDbContext _ctx;
        public FileRepository(EduDbContext ctx) => _ctx = ctx;

        public Task<FileResource?> GetByIdAsync(int id) =>
            _ctx.Files.Include(f => f.Prof).FirstOrDefaultAsync(f => f.Id == id);

        public Task<List<FileResource>> GetByProfSlugAsync(string slug) =>
            _ctx.Files.Include(f => f.Prof)
                      .Where(f => f.Prof.Slug == slug)
                      .OrderByDescending(f => f.CreatedAt)
                      .ToListAsync();

        public async Task AddAsync(FileResource file) => await _ctx.Files.AddAsync(file);
        public Task DeleteAsync(FileResource file) { _ctx.Files.Remove(file); return Task.CompletedTask; }
        public Task SaveAsync() => _ctx.SaveChangesAsync();
    }
}
