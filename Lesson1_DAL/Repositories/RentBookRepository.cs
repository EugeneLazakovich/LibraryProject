using Lesson1_DAL.Interfaces;
using Lesson1_DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<(Book book, BookRevision bookRevision, LibraryBooks libraryBook)> GetFullInfo(Guid bookId, Guid libraryId)
        {
            //var libraryBooks = await _dbContext.LibraryBooks.Include(x => x.BookRevision).Where(x => x.BookRevision.BookId == bookId).ToListAsync();
            var libraryBooks = await _dbContext.LibraryBooks
                .Include(c => c.BookRevision)
                .Include(c => c.Library)
                .Where(c => c.Library.Id == libraryId && c.BookRevision.BookId == bookId).ToListAsync();

            var book = libraryBooks.FirstOrDefault()?.BookRevision.Book;
            var bookRevision = libraryBooks.FirstOrDefault()?.BookRevision;
            var libraryBook = libraryBooks.FirstOrDefault();

            return (book, bookRevision, libraryBook);
        }
    }
}
