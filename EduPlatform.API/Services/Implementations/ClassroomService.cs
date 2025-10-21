using EduPlatform.API.DTOs.Classroom;
using EduPlatform.API.Models;
using EduPlatform.API.Repositories.Interfaces;
using EduPlatform.API.Services.Interfaces;

namespace EduPlatform.API.Services.Implementations
{
    public class ClassroomService : IClassroomService
    {
        private readonly IClassroomRepository _repo;

        public ClassroomService(IClassroomRepository repo)
        {
            _repo = repo;
        }

        private static string GenerateCode()
        {
            var guid = Guid.NewGuid().ToString("N").Substring(0, 6).ToUpper();
            return $"CL-{guid}";
        }

        public async Task<ClassResponse> CreateAsync(int profId, CreateClassRequest request)
        {
            var classroom = new Classroom
            {
                Name = request.Name,
                Description = request.Description,
                AccessCode = GenerateCode(),
                ProfId = profId
            };

            await _repo.AddAsync(classroom);
            await _repo.SaveAsync();

            return new ClassResponse
            {
                Id = classroom.Id,
                Name = classroom.Name,
                AccessCode = classroom.AccessCode,
                Description = classroom.Description,
                CreatedAt = classroom.CreatedAt
            };
        }

        public async Task<IEnumerable<ClassResponse>> GetByProfIdAsync(int profId)
        {
            var list = await _repo.GetByProfIdAsync(profId);
            return list.Select(c => new ClassResponse
            {
                Id = c.Id,
                Name = c.Name,
                AccessCode = c.AccessCode,
                Description = c.Description,
                CreatedAt = c.CreatedAt
            });
        }

        public async Task<ClassResponse?> GetByCodeAsync(string code)
        {
            var c = await _repo.GetByCodeAsync(code);
            return c == null ? null : new ClassResponse
            {
                Id = c.Id,
                Name = c.Name,
                AccessCode = c.AccessCode,
                Description = c.Description,
                CreatedAt = c.CreatedAt
            };
        }
    }
}
