using EduPlatform.API.Data;
using EduPlatform.API.Models;
using EduPlatform.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EduPlatform.API.Repositories.Implementations
{
    public class FileRepository : IFileRepository
    {
        private readonly EduDbContext _ctx;

        public FileRepository(EduDbContext ctx)
        {
            _ctx = ctx;
        }

        // =====================================================
        // ✅ 1️⃣ Récupérer un fichier par Id
        // =====================================================
        public Task<FileResource?> GetByIdAsync(int id) =>
            _ctx.FileResources
                .Include(f => f.Prof)
                .Include(f => f.Classroom)
                .FirstOrDefaultAsync(f => f.Id == id);

        // =====================================================
        // ✅ 2️⃣ Ajouter un nouveau fichier
        // =====================================================
        public async Task AddAsync(FileResource file)
        {
            await _ctx.FileResources.AddAsync(file);
        }

        // =====================================================
        // ✅ 3️⃣ Récupérer les fichiers d’un professeur (par slug)
        // =====================================================
        public Task<List<FileResource>> GetByProfSlugAsync(string slug) =>
            _ctx.FileResources
                .Include(f => f.Classroom)
                .Include(f => f.Prof)
                .Where(f => f.Prof.Slug == slug)
                .ToListAsync();

        // =====================================================
        // ✅ 4️⃣ Récupérer les fichiers d’une classe
        // =====================================================
        public Task<List<FileResource>> GetByClassIdAsync(int classId) =>
            _ctx.FileResources
                .Include(f => f.Prof)
                .Include(f => f.Classroom)
.Where(f => f.ClassroomId == classId)
                .ToListAsync();

        // =====================================================
        // ✅ 5️⃣ Sauvegarder les changements
        // =====================================================
        public Task SaveAsync() => _ctx.SaveChangesAsync();

        // =====================================================
        // ✅ 6️⃣ Supprimer un fichier (sans save immédiat)
        // =====================================================
        public void Remove(FileResource file)
        {
            _ctx.FileResources.Remove(file);
        }

        // =====================================================
        // ✅ 7️⃣ Supprimer un fichier (avec save immédiat)
        // =====================================================
        public async Task DeleteAsync(FileResource file)
        {
            _ctx.FileResources.Remove(file);
            await _ctx.SaveChangesAsync();
        }
    }
}
