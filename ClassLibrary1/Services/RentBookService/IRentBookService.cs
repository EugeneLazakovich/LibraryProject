using Lesson1_BL.DTOs;
using System;
using System.Threading.Tasks;

namespace Lesson1_BL.Services.RentBookService
{
    public interface IRentBookService
    {
        Task<RentBookDto> RentABook(Guid bookId, Guid clientId, Guid libraryId);
        Task<bool> ReturnABook(Guid bookRevisionId, Guid clientId);
    }
}