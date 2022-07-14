using Lesson1_BL.DTOs;
using Lesson1_DAL.Interfaces;
using Lesson1_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lesson1_BL.Services.BooksService
{
    public class BooksService : IBooksService
    {
        private readonly IGenericRepository<Book> _genericBookRepository;
        private readonly IBooksRepository _booksRepository;
        public BooksService(IGenericRepository<Book> genericBookRepository, IBooksRepository bookRepository)
        {
            _genericBookRepository = genericBookRepository;
            _booksRepository = bookRepository;
        }
        public async Task<Guid> AddBook(Book book)
        {
            return await _genericBookRepository.Add(book);
        }        

        public async Task<bool> DeleteByIdBook(Guid id)
        {
            return await _genericBookRepository.DeleteById(id);
        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            return await _genericBookRepository.GetAll();
        }

        public async Task<Book> GetByIdBook(Guid id)
        {
            return await _genericBookRepository.GetById(id);
        }

        public async Task<bool> UpdateBook(Book book)
        {
            return await _genericBookRepository.Update(book);
        }

        public async Task<IEnumerable<Book>> GetBooksByCity(string cityName)
        {
            return (await _genericBookRepository.GetAll());
        }

        public async Task<IEnumerable<Book>> GetMostReadableBooks(int top)
        {
            return (await _genericBookRepository.GetAll());
        }

        public async Task<BookWithRevisionsDto> GetBookFullInfo(Guid id)
        {
            var result = await _booksRepository.GetFullInfo(id);

            return MapTupleToBookDto(result);
        }

        private BookWithRevisionsDto MapTupleToBookDto((Book book, IEnumerable<BookRevision> bookRevisions) result)
        {
            return new BookWithRevisionsDto
            {
                Author = result.book?.Author,
                BookId = result.book.Id,
                Title = result.book.Title,
                BookRevisions = MapRevisions(result.bookRevisions)
            };
        }

        private IEnumerable<BookRevisionDto> MapRevisions(IEnumerable<BookRevision> bookRevisions)
        {
            return bookRevisions.Select(c => new BookRevisionDto
            {
                Price = c.Price,
                PagesCount = c.PagesCount,
                YearOfPublishing = c.YearOfPublishing
            });
        }
    }
}
