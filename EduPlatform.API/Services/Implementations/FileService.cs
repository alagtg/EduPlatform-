using EduPlatform.API.DTOs.Files;
using EduPlatform.API.Models;
using EduPlatform.API.Repositories.Interfaces;
using EduPlatform.API.Services.Interfaces;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;

namespace EduPlatform.API.Services.Implementations
{
    public class FileService : IFileService
    {
        private readonly IFileRepository _fileRepo;
        private readonly IProfRepository _profRepo;
        private readonly IConfiguration _cfg;
        private readonly string _root;
        private readonly string _folder;

        public FileService(IFileRepository fileRepo, IProfRepository profRepo, IConfiguration cfg, IWebHostEnvironment env)
        {
            _fileRepo = fileRepo; _profRepo = profRepo; _cfg = cfg;
            _root = Path.Combine(env.ContentRootPath, _cfg["Upload:Root"] ?? "wwwroot");
            _folder = _cfg["Upload:Folder"] ?? "Uploads";
            Directory.CreateDirectory(Path.Combine(_root, _folder));
        }

        public async Task<FileResponse> UploadAsync(int profId, FileUploadRequest request)
        {
            var prof = await _profRepo.GetByIdAsync(profId) ?? throw new KeyNotFoundException("Prof introuvable");
            var ext = Path.GetExtension(request.File.FileName);
            var fileName = $"{Guid.NewGuid()}{ext}";

            var relPath = Path.Combine(_folder, fileName).Replace("\\", "/");
            var absPath = Path.Combine(_root, relPath.Replace("/", Path.DirectorySeparatorChar.ToString()));

            using (var stream = new FileStream(absPath, FileMode.Create))
            {
                await request.File.CopyToAsync(stream);
            }

            var entity = new FileResource
            {
                ProfId = prof.Id,
                Title = request.Title,
                Type = request.Type,
                Path = "/" + relPath
            };

            await _fileRepo.AddAsync(entity);
            await _fileRepo.SaveAsync();

            return new FileResponse
            {
                Id = entity.Id,
                Title = entity.Title,
                Type = entity.Type,
                Url = entity.Path,
                DownloadCount = entity.DownloadCount,
                CreatedAt = entity.CreatedAt,
                ProfSlug = prof.Slug
            };
        }

        public async Task<List<FileResponse>> ListByProfSlugAsync(string slug, string baseUrl)
        {
            var list = await _fileRepo.GetByProfSlugAsync(slug);
            return list.Select(f => new FileResponse
            {
                Id = f.Id,
                Title = f.Title,
                Type = f.Type,
                Url = CombineBase(baseUrl, f.Path),
                DownloadCount = f.DownloadCount,
                CreatedAt = f.CreatedAt,
                ProfSlug = f.Prof.Slug
            }).ToList();
        }

        public async Task<(Stream stream, string fileName, string contentType)?> GetForDownloadAsync(int id)
        {
            var fr = await _fileRepo.GetByIdAsync(id);
            if (fr == null) return null;

            var absPath = Path.Combine(_root, fr.Path.TrimStart('/').Replace("/", Path.DirectorySeparatorChar.ToString()));
            if (!File.Exists(absPath)) return null;

            new FileExtensionContentTypeProvider().TryGetContentType(absPath, out var ct);
            ct ??= "application/octet-stream";

            fr.DownloadCount++;
            await _fileRepo.SaveAsync();

            return (File.OpenRead(absPath), Path.GetFileName(absPath), ct);
        }

        public async Task DeleteAsync(int id, int profId)
        {
            var fr = await _fileRepo.GetByIdAsync(id) ?? throw new KeyNotFoundException("Fichier introuvable");
            if (fr.ProfId != profId) throw new UnauthorizedAccessException("Non autorisé");

            var absPath = Path.Combine(_root, fr.Path.TrimStart('/').Replace("/", Path.DirectorySeparatorChar.ToString()));
            if (File.Exists(absPath)) File.Delete(absPath);

            await _fileRepo.DeleteAsync(fr);
            await _fileRepo.SaveAsync();
        }

        private static string CombineBase(string baseUrl, string rel) =>
            baseUrl.TrimEnd('/') + "/" + rel.TrimStart('/');
    }
}
