using EduPlatform.API.Data;
using EduPlatform.API.Models;
using EduPlatform.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EduPlatform.API.Repositories.Implementations
{
    public class ClassroomRepository : IClassroomRepository
    {
        private readonly EduDbContext _ctx;
        public ClassroomRepository(EduDbContext ctx) => _ctx = ctx;

        public async Task AddAsync(Classroom c) => await _ctx.Classrooms.AddAsync(c);

        public async Task<IEnumerable<Classroom>> GetByProfIdAsync(int profId)
        {
            return await _ctx.Classrooms
                .Where(c => c.ProfId == profId)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
        }

        public async Task<Classroom?> GetByCodeAsync(string code)
        {
            return await _ctx.Classrooms
                .Include(c => c.Prof)
                .FirstOrDefaultAsync(c => c.AccessCode == code);
        }

        public async Task SaveAsync() => await _ctx.SaveChangesAsync();
    }
}
