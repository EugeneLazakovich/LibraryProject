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
    public class BooksRepository : IBooksRepository
    {
        private readonly EFCoreDbContext _dbContext;
        public BooksRepository(EFCoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<(Book book, IEnumerable<BookRevision> bookRevisions)> GetFullInfo(Guid id)
        {
            var result = await _dbContext.BookRevisions.Include(c => c.Book).Where(c => c.BookId == id).ToListAsync();

            var book = result.FirstOrDefault()?.Book;

            return (book, result);
        }
    }
}
