using Microsoft.EntityFrameworkCore;
using Lesson1_DAL.Models;
using Lesson1_DAL.Migrations;

namespace Lesson1_DAL
{
    public class EFCoreDbContext : DbContext
    {
        public DbSet<Location> Locations { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Library> Libraries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<User> Clients { get; set; }
        public DbSet<BookRevision> BookRevisions { get; set; }
        public DbSet<LibraryBooks> LibraryBooks { get; set; }
        public DbSet<RentBook> RentBooks { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected EFCoreDbContext()
        {
        }

        public EFCoreDbContext(DbContextOptions<EFCoreDbContext> options) : base(options)  
        {
            //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
    }
}
