using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace Git.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Repository> Repositories { get; set; }

        public DbSet<Commit> Commits { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=Git;Integrated Security=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Repository>(entity =>
            {
                entity.HasMany(x => x.Commits)
                      .WithOne(x => x.Repository)
                      .HasForeignKey(x => x.RepositoryId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasMany(x => x.Repositories)
                      .WithOne(x => x.Owner)
                      .HasForeignKey(x => x.OwnerId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasMany(x => x.Commits)
                      .WithOne(x => x.Creator)
                      .HasForeignKey(x => x.CreatorId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}