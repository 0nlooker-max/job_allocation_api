using Microsoft.EntityFrameworkCore;
using joballo_api.Models;

namespace joballo_api.Data
{
    public class JobContext : DbContext
    {
        public JobContext(DbContextOptions<JobContext> options) : base(options)
        {
        }

        public DbSet<JobPosition> JobPositions { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<JobPosition>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
                entity.Property(e => e.BeginningSalary).HasPrecision(18, 2);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}