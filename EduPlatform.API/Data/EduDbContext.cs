using EduPlatform.API.Models;
using Microsoft.EntityFrameworkCore;

namespace EduPlatform.API.Data
{
    public class EduDbContext : DbContext
    {
        public EduDbContext(DbContextOptions<EduDbContext> options) : base(options) { }

        public DbSet<Prof> Profs => Set<Prof>();
        public DbSet<FileResource> Files => Set<FileResource>();
        public DbSet<Comment> Comments => Set<Comment>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Prof>()
                .HasIndex(p => p.Email)
                .IsUnique();

            modelBuilder.Entity<Prof>()
                .HasIndex(p => p.Slug)
                .IsUnique();

            modelBuilder.Entity<FileResource>()
                .HasOne(f => f.Prof)
                .WithMany(p => p.Files)
                .HasForeignKey(f => f.ProfId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
