using Lesson1_DAL;
using Lesson1_DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lesson1_BL
{
    public interface IBooksService
    {
        Task<IEnumerable<Book>> GetAllBooks();
        Task<Book> GetByIdBook(Guid id);
        Task<bool> DeleteByIdBook(Guid id);
        Task<bool> UpdateBook(Book book);
        Task<Guid> AddBook(Book book);
        Task<IEnumerable<Book>> GetBooksByCity(string cityName);
        Task<IEnumerable<Book>> GetMostReadableBooks(int top);
    }
}
