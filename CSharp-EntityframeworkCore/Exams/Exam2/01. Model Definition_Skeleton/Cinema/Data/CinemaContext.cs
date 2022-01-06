namespace Cinema.Data
{
    using Cinema.Data.Models;
    using Microsoft.EntityFrameworkCore;

    public class CinemaContext : DbContext
    {
        public CinemaContext()  { }

        public CinemaContext(DbContextOptions options)
            : base(options)   { }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<Projection> Projections { get; set; }

        public DbSet<Ticket> Tickets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasOne(x => x.Customer)
                      .WithMany(x => x.Tickets)
                      .HasForeignKey(x => x.CustomerId);

                entity.HasOne(x => x.Projection)
                       .WithMany(x => x.Tickets)
                       .HasForeignKey(x => x.ProjectionId);
            });
        }
    }
}