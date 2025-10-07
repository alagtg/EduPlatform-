using EduPlatform.API.DTOs.Auth;
using EduPlatform.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EduPlatform.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;
        private readonly IProfService _profSvc;

        public AuthController(IAuthService auth, IProfService profSvc)
        {
            _auth = auth; _profSvc = profSvc;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest req)
        {
            var resp = await _auth.LoginAsync(req);
            if (resp == null) return Unauthorized("Email ou mot de passe invalide");
            return Ok(resp);
        }

        // Endpoint pour créer un prof (à sécuriser en prod ou à exécuter une seule fois)
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] DTOs.Prof.CreateProfRequest req)
        {
            var created = await _profSvc.CreateAsync(req);
            return Ok(created);
        }
    }
}
