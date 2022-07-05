using Lesson1_DAL;
using System;
using System.Collections.Generic;

namespace Lesson1_BL
{
    public interface IBooksService
    {
        IEnumerable<Book> GetAllBooks();
        Book GetByIdBook(Guid id);
        bool DeleteByIdBook(Guid id);
        bool UpdateBook(Book book);
        Guid AddBook(Book book);
        IEnumerable<Book> GetBooksByCity(string cityName);
        IEnumerable<Book> GetMostReadableBooks(int top);
    }
}
