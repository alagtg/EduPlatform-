using EduPlatform.API.Data;
using EduPlatform.API.Models;
using EduPlatform.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EduPlatform.API.Repositories.Implementations
{
    public class ProfRepository : IProfRepository
    {
        private readonly EduDbContext _ctx;
        public ProfRepository(EduDbContext ctx) => _ctx = ctx;

        public Task<Prof?> GetByEmailAsync(string email) =>
            _ctx.Profs.FirstOrDefaultAsync(p => p.Email == email);

        public Task<Prof?> GetByIdAsync(int id) =>
            _ctx.Profs.FindAsync(id).AsTask();

        public Task<Prof?> GetBySlugAsync(string slug) =>
            _ctx.Profs.FirstOrDefaultAsync(p => p.Slug == slug);

        public async Task AddAsync(Prof prof)
        {
            await _ctx.Profs.AddAsync(prof);
        }
        public Task<List<Prof>> GetAllAsync() =>
            _ctx.Profs.ToListAsync();

        public Task<bool> EmailExistsAsync(string email) =>
            _ctx.Profs.AnyAsync(p => p.Email == email);

        public Task<bool> SlugExistsAsync(string slug) =>
            _ctx.Profs.AnyAsync(p => p.Slug == slug);

        public Task SaveAsync() => _ctx.SaveChangesAsync();
    }
}
