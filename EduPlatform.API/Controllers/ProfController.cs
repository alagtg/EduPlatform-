using System.Security.Claims;
using EduPlatform.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduPlatform.API.Controllers
{
    [ApiController]
    [Route("api/prof")]
    [Authorize]
    public class ProfController : ControllerBase
    {
        private readonly IProfService _svc;

        public ProfController(IProfService svc) { _svc = svc; }

        private int CurrentProfId => int.Parse(User.FindFirstValue("profId")!);

        [HttpGet("me")]
        public async Task<IActionResult> Me()
        {
            var me = await _svc.GetByIdAsync(CurrentProfId);
            return Ok(me);
        }
        [HttpGet("all")]
        [AllowAnonymous] // ou [Authorize] selon ton besoin
        public async Task<IActionResult> GetAll()
        {
            var profs = await _svc.GetAllAsync();
            return Ok(profs);
        }

    }
}
