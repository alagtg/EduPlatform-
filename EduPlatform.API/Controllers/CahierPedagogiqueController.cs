using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EduPlatform.API.Data;
using EduPlatform.API.Models;

namespace EduPlatform.Controllers
{
    [ApiController]
    [Route("api/cahiers")]
    public class CahierPedagogiqueController : ControllerBase
    {
        private readonly EduDbContext _context;
        private readonly IWebHostEnvironment _env;

        public CahierPedagogiqueController(EduDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // ✅ UPLOAD : ajoute toujours un NOUVEAU cahier
        [HttpPost("upload-cahier")]
        public async Task<IActionResult> UploadCahierPedagogique(
            [FromForm] IFormFile file,
            [FromForm] int classId,
            [FromForm] string? fileName = null
        )
        {
            if (file == null || classId <= 0)
                return BadRequest("Fichier ou classe invalide.");

            var classroom = await _context.Classrooms.FindAsync(classId);
            if (classroom == null)
                return NotFound("Classe introuvable.");

            // ✅ Création du dossier s’il n’existe pas
            var webRoot = _env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            var folder = Path.Combine(webRoot, "uploads", "cahiers");
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            // 📄 Sauvegarde du fichier
            var uniqueName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
            var filePath = Path.Combine(folder, uniqueName);

            try
            {
                await using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Erreur fichier : {ex.Message}");
                return StatusCode(500, "Erreur lors de la sauvegarde du fichier : " + ex.Message);
            }

            var fileUrl = $"{Request.Scheme}://{Request.Host}/uploads/cahiers/{uniqueName}";

            // ✅ On ajoute toujours un nouveau cahier sans supprimer les anciens
            var cahier = new CahierPedagogique
            {
                ClassId = classId,
                FileName = string.IsNullOrWhiteSpace(fileName) ? file.FileName : fileName,
                FilePath = filePath,
                FileUrl = fileUrl,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            _context.CahiersPedagogiques.Add(cahier);
            await _context.SaveChangesAsync();

            Console.WriteLine($"💾 Nouveau cahier ajouté pour la classe {classId}");
            return Ok(new { message = "✅ Cahier pédagogique ajouté avec succès !" });
        }

        // ✅ SUPPRESSION
        [HttpDelete("delete-cahier/{id}")]
        public async Task<IActionResult> DeleteCahierPedagogique(int id)
        {
            var cahier = await _context.CahiersPedagogiques.FindAsync(id);
            if (cahier == null)
                return NotFound("Cahier introuvable.");

            if (!string.IsNullOrEmpty(cahier.FilePath) && System.IO.File.Exists(cahier.FilePath))
                System.IO.File.Delete(cahier.FilePath);

            _context.CahiersPedagogiques.Remove(cahier);
            await _context.SaveChangesAsync();

            return Ok(new { message = "🗑️ Cahier supprimé avec succès." });
        }

        // ✅ GET cahier par ID
        [HttpGet("cahier/{id}")]
        public async Task<IActionResult> GetCahierById(int id)
        {
            var cahier = await _context.CahiersPedagogiques
                .Where(c => c.Id == id)
                .Select(c => new
                {
                    c.Id,
                    c.FileName,
                    c.FileUrl,
                    c.ClassId,
                    c.CreatedAt,
                    c.UpdatedAt
                })
                .FirstOrDefaultAsync();

            if (cahier == null)
                return NotFound("Cahier introuvable.");

            return Ok(cahier);
        }

        // ✅ GET tous les cahiers d’une classe
        [HttpGet("by-class/{classId}")]
        public async Task<IActionResult> GetCahiersByClass(int classId)
        {
            var cahiers = await _context.CahiersPedagogiques
                .Where(c => c.ClassId == classId)
                .OrderByDescending(c => c.CreatedAt)
                .Select(c => new
                {
                    c.Id,
                    c.FileName,
                    c.FileUrl,
                    c.CreatedAt
                })
                .ToListAsync();

            return Ok(cahiers);
        }
    }
}
