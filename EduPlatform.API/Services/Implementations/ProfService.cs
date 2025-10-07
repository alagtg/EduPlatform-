using EduPlatform.API.DTOs.Prof;
using EduPlatform.API.Models;
using EduPlatform.API.Repositories.Interfaces;
using EduPlatform.API.Services.Interfaces;

namespace EduPlatform.API.Services.Implementations
{
    public class ProfService : IProfService
    {
        private readonly IProfRepository _repo;
        private readonly IAuthService _auth;

        public ProfService(IProfRepository repo, IAuthService auth)
        {
            _repo = repo; _auth = auth;
        }

        public async Task<ProfResponse> CreateAsync(CreateProfRequest request)
        {
            if (await _repo.EmailExistsAsync(request.Email))
                throw new InvalidOperationException("Email déjà utilisé.");
            if (await _repo.SlugExistsAsync(request.Slug))
                throw new InvalidOperationException("Slug déjà utilisé.");

            var prof = new Prof
            {
                Name = request.Name,
                Email = request.Email,
                Slug = request.Slug,
                PasswordHash = _auth.HashPassword(request.Password)
            };
            await _repo.AddAsync(prof);
            await _repo.SaveAsync();

            return new ProfResponse
            {
                Id = prof.Id,
                Name = prof.Name,
                Email = prof.Email,
                Slug = prof.Slug,
                CreatedAt = prof.CreatedAt
            };
        }
        public async Task<List<ProfResponse>> GetAllAsync()
        {
            var profs = await _repo.GetAllAsync();
            return profs.Select(p => new ProfResponse
            {
                Id = p.Id,
                Name = p.Name,
                Email = p.Email,
                Slug = p.Slug,
                CreatedAt = p.CreatedAt
            }).ToList();
        }

        public async Task<ProfResponse?> GetByIdAsync(int id)
        {
            var p = await _repo.GetByIdAsync(id);
            return p == null ? null : new ProfResponse { Id = p.Id, Name = p.Name, Email = p.Email, Slug = p.Slug, CreatedAt = p.CreatedAt };
        }

        public async Task<ProfResponse?> GetBySlugAsync(string slug)
        {
            var p = await _repo.GetBySlugAsync(slug);
            return p == null ? null : new ProfResponse { Id = p.Id, Name = p.Name, Email = p.Email, Slug = p.Slug, CreatedAt = p.CreatedAt };
        }
    }
}
