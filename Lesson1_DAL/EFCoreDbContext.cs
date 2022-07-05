using Microsoft.EntityFrameworkCore;

namespace Lesson1_DAL
{
    public class EFCoreDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Library> Libraries { get; set; }
        public DbSet<Location> Locations { get; set; }

        public EFCoreDbContext(DbContextOptions<EFCoreDbContext> options) : base(options)  
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Location>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.XCoordinate).IsRequired();
                entity.Property(e => e.YCoordinate).IsRequired();
            });

            modelBuilder.Entity<Library>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.HasOne(d => d.Location)
                  .WithOne(p => p.Library);
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FirstName).IsRequired();
                entity.Property(e => e.LastName).IsRequired();
                entity.Property(e => e.IsBlocked).IsRequired();
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired();
                entity.Property(e => e.Author).IsRequired();
                entity.Property(e => e.PagesCount).IsRequired();
                entity.Property(e => e.DateOfRent);
                entity.Property(e => e.DaysForRent).IsRequired();
                entity.Property(e => e.IsDamaged).IsRequired();
                entity.Property(e => e.IsDelayed).IsRequired();
                entity.Property(e => e.RentCount).IsRequired();
                entity.HasOne(d => d.Library)
                  .WithMany(p => p.Books);
                entity.HasOne(d => d.Client)
                  .WithMany(p => p.Books);
            });
        }
    }
}
