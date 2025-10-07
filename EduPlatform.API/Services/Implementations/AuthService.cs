using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EduPlatform.API.DTOs.Auth;
using EduPlatform.API.Repositories.Interfaces;
using EduPlatform.API.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace EduPlatform.API.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IProfRepository _profRepo;
        private readonly IConfiguration _cfg;

        public AuthService(IProfRepository profRepo, IConfiguration cfg)
        {
            _profRepo = profRepo;
            _cfg = cfg;
        }

        public async Task<LoginResponse?> LoginAsync(LoginRequest request)
        {
            var prof = await _profRepo.GetByEmailAsync(request.Email);
            if (prof is null) return null;

            if (!VerifyPassword(request.Password, prof.PasswordHash)) return null;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_cfg["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, prof.Id.ToString()),
                new Claim("profId", prof.Id.ToString()),
                new Claim("slug", prof.Slug),
                new Claim(JwtRegisteredClaimNames.Email, prof.Email)
            };

            var token = new JwtSecurityToken(
                issuer: _cfg["Jwt:Issuer"],
                audience: _cfg["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: creds
            );

            return new LoginResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ProfId = prof.Id,
                ProfName = prof.Name,
                ProfSlug = prof.Slug
            };
        }

        // Simplifié: PBKDF2/bcrypt/Argon2 recommandé en prod
        public string HashPassword(string password) =>
            Convert.ToBase64String(Encoding.UTF8.GetBytes(password));

        public bool VerifyPassword(string password, string passwordHash) =>
            HashPassword(password) == passwordHash;
    }
}
