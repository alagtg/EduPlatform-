using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EduPlatform.API.Services.Interfaces;
using EduPlatform.API.DTOs.Classroom;
using System.Security.Claims;

namespace EduPlatform.API.Controllers
{
    [ApiController]
    [Route("api/classrooms")]
    [Authorize]
    public class ClassroomController : ControllerBase
    {
        private readonly IClassroomService _svc;

        public ClassroomController(IClassroomService svc)
        {
            _svc = svc;
        }

        private int CurrentProfId => int.Parse(User.FindFirstValue("profId")!);

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateClassRequest dto)
        {
            var result = await _svc.CreateAsync(CurrentProfId, dto);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetByProf()
        {
            var result = await _svc.GetByProfIdAsync(CurrentProfId);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("{code}")]
        public async Task<IActionResult> GetByCode(string code)
        {
            var result = await _svc.GetByCodeAsync(code);
            if (result == null)
                return NotFound(new { message = "Classe introuvable" });

            return Ok(result);
        }
    }
}
