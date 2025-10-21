using EduPlatform.API.DTOs.Files;
using EduPlatform.API.Models;
using EduPlatform.API.Repositories.Interfaces;
using EduPlatform.API.Services.Interfaces;

namespace EduPlatform.API.Services.Implementations
{
    public class FileService : IFileService
    {
        private readonly IFileRepository _repo;
        private readonly IWebHostEnvironment _env;

        public FileService(IFileRepository repo, IWebHostEnvironment env)
        {
            _repo = repo;
            _env = env;
        }

        // =====================================================
        // ✅ 1️⃣ Upload de fichier
        // =====================================================
        public async Task<FileResponse> UploadAsync(int profId, FileUploadRequest request)
        {
            // 📂 Dossier de destination
            var uploadsDir = Path.Combine(_env.WebRootPath ?? "wwwroot", "uploads");
            if (!Directory.Exists(uploadsDir))
                Directory.CreateDirectory(uploadsDir);

            // 📄 Nom unique du fichier
            var uniqueName = $"{Guid.NewGuid()}_{request.File.FileName}";
            var filePath = Path.Combine(uploadsDir, uniqueName);

            // 💾 Sauvegarde physique
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await request.File.CopyToAsync(stream);
            }

            // 🗂 Enregistrement en base
            var file = new FileResource
            {
                Title = request.Title,
                FileName = uniqueName,
                FilePath = filePath,
                ProfId = profId,
                ClassroomId = request.ClassId,
                Type = request.Type,
                CreatedAt = DateTime.UtcNow
            };

            await _repo.AddAsync(file);
            await _repo.SaveAsync();

            return new FileResponse
            {
                Id = file.Id,
                Title = file.Title,
                FileName = file.FileName,
                FileUrl = $"/uploads/{file.FileName}",
                Type = file.Type.ToString(),
                ClassroomName = file.Classroom?.Name,
                CreatedAt = file.CreatedAt
            };
        }

        // =====================================================
        // ✅ 2️⃣ Suppression de fichier
        // =====================================================
        public async Task DeleteAsync(int id, int profId)
        {
            var file = await _repo.GetByIdAsync(id);
            if (file == null || file.ProfId != profId)
                throw new Exception("Fichier introuvable ou non autorisé.");

            if (File.Exists(file.FilePath))
                File.Delete(file.FilePath);

            _repo.Remove(file);
            await _repo.SaveAsync();
        }

        // =====================================================
        // ✅ 3️⃣ Téléchargement d’un fichier
        // =====================================================
        public async Task<(Stream stream, string fileName, string contentType)?> GetForDownloadAsync(int id)
        {
            var file = await _repo.GetByIdAsync(id);
            if (file == null || !File.Exists(file.FilePath))
                return null;

            var stream = new FileStream(file.FilePath, FileMode.Open, FileAccess.Read);
            var contentType = "application/octet-stream";
            return (stream, file.FileName, contentType);
        }

        // =====================================================
        // ✅ 4️⃣ Liste par professeur (slug)
        // =====================================================
        public async Task<IEnumerable<FileResponse>> ListByProfSlugAsync(string slug, string baseUrl)
        {
            var files = await _repo.GetByProfSlugAsync(slug);

            return files.Select(f => new FileResponse
            {
                Id = f.Id,
                Title = f.Title,
                FileName = f.FileName,
                FileUrl = $"{baseUrl}/uploads/{f.FileName}",
                Type = f.Type.ToString(),
                ClassroomName = f.Classroom?.Name,
                CreatedAt = f.CreatedAt
            });
        }

        // =====================================================
        // ✅ 5️⃣ Liste par classe
        // =====================================================
        public async Task<IEnumerable<FileResponse>> ListByClassAsync(int classId, string baseUrl)
        {
            var files = await _repo.GetByClassIdAsync(classId);

            return files.Select(f => new FileResponse
            {
                Id = f.Id,
                Title = f.Title,
                FileName = f.FileName,
                FileUrl = $"{baseUrl}/uploads/{f.FileName}",
                Type = f.Type.ToString(),
                ClassroomName = f.Classroom?.Name,
                CreatedAt = f.CreatedAt
            });
        }
    }
}
