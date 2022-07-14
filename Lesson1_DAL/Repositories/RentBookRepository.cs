using Lesson1_DAL.Interfaces;
using Lesson1_DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson1_DAL.Repositories
{
    public class RentBookRepository : IRentBookRepository
    {
        private readonly EFCoreDbContext _dbContext;
        public RentBookRepository(EFCoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<(Book book, BookRevision bookRevision, IEnumerable<LibraryBooks> libraryBooks)> GetFullInfo(Guid bookId)
        {
            var libraryBooks = await _dbContext.LibraryBooks.Include(x => x.BookRevision).Where(x => x.BookRevision.BookId == bookId).ToListAsync();

            var book = libraryBooks.FirstOrDefault()?.BookRevision.Book;
            var bookRevision = libraryBooks?.FirstOrDefault()?.BookRevision;

            return (book, bookRevision, libraryBooks);
        }
    }
}
