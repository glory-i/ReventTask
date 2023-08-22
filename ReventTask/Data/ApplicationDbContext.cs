using Microsoft.EntityFrameworkCore;
using ReventTask.Models;
using System.Security.Principal;

namespace ReventTask.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Classroom> Classrooms { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //base.OnModelCreating(builder);
            builder.Entity<Teacher>(entity =>
            {
                entity.HasKey(t => t.Id);
                entity.HasMany(t => t.Students).WithOne(s => s.Teacher);
                entity.HasMany(t => t.Classrooms).WithOne(c => c.Teacher);
            });


            builder.Entity<Classroom>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.HasOne(c => c.Teacher).WithMany(t => t.Classrooms).HasForeignKey(c=>c.TeacherId);
                entity.HasMany(c => c.Students).WithOne(s => s.Classroom);

            });


            builder.Entity<Student>(entity =>
            {
                entity.HasKey(s => s.Id);
                entity.HasOne(s => s.Classroom).WithMany(c => c.Students).HasForeignKey(s => s.ClassromId);
                entity.HasOne(s => s.Teacher).WithMany(t => t.Students).HasForeignKey(s=>s.TeacherId);

            });

        }
    }
}
