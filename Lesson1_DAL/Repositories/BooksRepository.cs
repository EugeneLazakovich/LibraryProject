using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lesson1_DAL
{
    public class BooksRepository : IBooksRepository
    {
        private readonly EFCoreDbContext _dbContext;
        public BooksRepository(EFCoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Guid Add(Book book)
        {
            book.Id = Guid.NewGuid();
            book.Client = null;
            book.DateOfRent = null;
            book.DaysForRent = 0;
            book.IsDamaged = false;
            book.IsDelayed = false;
            book.RentCount = 0;
            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();

            return book.Id;
        }

        public bool DeleteById(Guid id)
        {
            var book = new Book { Id = id };
            _dbContext.Entry(book).State = EntityState.Deleted;

            return _dbContext.SaveChanges() != 0;
        }

        public IEnumerable<Book> GetAll()
        {
            return _dbContext.Books.ToList();
        }

        public Book GetById(Guid id)
        {
            return _dbContext.Books.Where(c => c.Id == id).FirstOrDefault();
        }

        public bool Update(Book book)
        {
            _dbContext.Entry(book).State = EntityState.Modified;

            return _dbContext.SaveChanges() != 0;
        }
    }
}
