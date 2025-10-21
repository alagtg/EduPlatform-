using EduPlatform.API.Models;
using Microsoft.EntityFrameworkCore;

namespace EduPlatform.API.Data
{
    public class EduDbContext : DbContext
    {
        public EduDbContext(DbContextOptions<EduDbContext> options) : base(options) { }

        public DbSet<Prof> Profs { get; set; }
        public DbSet<FileResource> FileResources { get; set; }
        public DbSet<Classroom> Classrooms { get; set; }
        public DbSet<Comment> Comments { get; set; }
        // ✅ ajouter la table
        public DbSet<CahierPedagogique> CahiersPedagogiques { get; set; }

        // ✅ tes autres tables
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Classroom>()
                .HasMany(c => c.Files)
                .WithOne(f => f.Classroom)
                .HasForeignKey(f => f.ClassroomId);

            modelBuilder.Entity<Classroom>()
                .HasMany(c => c.Cahiers)
                .WithOne(c => c.Classroom)
                .HasForeignKey(c => c.ClassId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Prof>()
                .HasMany<FileResource>()
                .WithOne(f => f.Prof)
                .HasForeignKey(f => f.ProfId);
        }
    }
}
