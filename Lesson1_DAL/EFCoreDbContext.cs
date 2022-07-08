using Microsoft.EntityFrameworkCore;
using Lesson1_DAL.Models;

namespace Lesson1_DAL
{
    public class EFCoreDbContext : DbContext
    {
        public DbSet<Location> Locations { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Library> Libraries { get; set; }        
        //public DbSet<City> Cities { get; set; }
        //public DbSet<Client> Clients { get; set; }
        //public DbSet<BookRevision> BookRevisions { get; set; }
        //public DbSet<LibraryBooks> LibraryBooks { get; set; }
        //public DbSet<RentBook> RentBooks { get; set; }

        public EFCoreDbContext(DbContextOptions<EFCoreDbContext> options) : base(options)  
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}
