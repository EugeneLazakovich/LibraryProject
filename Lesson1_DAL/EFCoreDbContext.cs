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

        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-0Q7NADI\\SQLEXPRESS;Initial Catalog=NetCoreDB;Integrated Security=True");
        }*/

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*modelBuilder.Entity<Book>().ToTable("Book", "dbo");
            modelBuilder.Entity<Client>().ToTable("Client", "dbo");
            modelBuilder.Entity<City>().ToTable("City", "dbo");
            modelBuilder.Entity<Library>().ToTable("Library", "dbo");
            modelBuilder.Entity<Location>().ToTable("Location", "dbo");*/
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
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired();
                entity.Property(e => e.Author).IsRequired();
                entity.Property(e => e.PagesCount).IsRequired();
                entity.Property(e => e.IsRent).IsRequired();
                entity.Property(e => e.RentCount).IsRequired();
                entity.HasOne(d => d.Library)
                  .WithMany(p => p.Books);
                entity.HasOne(d => d.Client)
                  .WithMany(p => p.Books);
            });
        }
    }
}
