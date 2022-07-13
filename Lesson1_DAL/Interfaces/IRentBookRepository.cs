using Lesson1_DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lesson1_DAL.Interfaces
{
    public interface IRentBookRepository
    {
        Task<(Book book, BookRevision bookRevision, IEnumerable<LibraryBooks> libraryBooks)> GetFullInfo(Guid bookId);
    }
}