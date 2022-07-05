using Lesson1_DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lesson1_BL
{
    public class BooksService : IBooksService
    {
        private readonly IBooksRepository _booksRepository; 
        public BooksService(IBooksRepository booksRepository)
        {
            _booksRepository = booksRepository;
        }
        public Guid AddBook(Book book)
        {
            ValidateBookState(book);
            return _booksRepository.Add(book);
        }        

        public bool DeleteByIdBook(Guid id)
        {
            return _booksRepository.DeleteById(id);
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _booksRepository.GetAll();
        }

        public Book GetByIdBook(Guid id)
        {
            return _booksRepository.GetById(id);
        }

        public bool UpdateBook(Book book)
        {
            ValidateBookState(book);

            return _booksRepository.Update(book);
        }

        public IEnumerable<Book> GetBooksByCity(string cityName)
        {
            return _booksRepository.GetAll().Where(book => book.Library != null && book.Library.City != null && book.Library.City.Name == cityName);
        }

        public IEnumerable<Book> GetMostReadableBooks(int top)
        {
            return _booksRepository.GetAll().OrderBy(c => c.RentCount).Take(top);
        }

        private static void ValidateBookState(Book book)
        {
            if (book.PagesCount < 10 || book.PagesCount > 2000)
            {
                throw new ArgumentException("Invalid pages count!");
            }
        }
    }
}
