using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lesson1_DAL
{
    public class LibrariesRepository : ILibrariesRepository
    {
        private readonly EFCoreDbContext _dbContext;
        public LibrariesRepository(EFCoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Guid Add(Library library)
        {
            library.Id = Guid.NewGuid();
            library.Books = new List<Book>();
            _dbContext.Add(library);
            _dbContext.SaveChanges();

            return library.Id;
        }

        public IEnumerable<Library> GetAll()
        {
            return _dbContext.Libraries.ToList();
        }
    }
}
