using iktprojekt.Models;
using Microsoft.EntityFrameworkCore;

namespace iktprojekt.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Member> Members { get; set; } 
        public DbSet<Project> Projects { get; set; }
        public DbSet<Models.PTask> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Models.PTask>()
                .HasOne(t => t.Member)
                .WithMany(m => m.Tasks)
                .HasForeignKey(t => t.MemberId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Models.PTask>()
                .HasOne(t => t.Project)
                .WithMany(p => p.Tasks)
                .HasForeignKey(t => t.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
