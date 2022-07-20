using Lesson1_DAL.Models;
using System;
using System.Threading.Tasks;

namespace Lesson1_DAL.Interfaces
{
    public interface IRentBookRepository
    {
        Task<(Book book, BookRevision bookRevision, LibraryBooks libraryBook)> GetFullInfo(Guid bookId, Guid libraryId);
    }
}