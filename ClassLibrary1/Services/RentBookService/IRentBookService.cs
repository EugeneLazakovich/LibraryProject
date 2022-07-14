using Lesson1_BL.DTOs;
using Lesson1_DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lesson1_BL.Services.RentBookService
{
    public interface IRentBookService
    {
        Task<Guid> AddRentBook(RentBook rentBook);
        Task<bool> DeleteByIdRentBook(Guid id);
        Task<IEnumerable<RentBook>> GetAllRentBooks();
        Task<RentBook> GetByIdRentBook(Guid id);
        Task<bool> UpdateRentBook(RentBook rentBook);
        Task<RentBookDto> RentABook(Location location, Guid bookId, Guid clientId, int top);
        Task<bool> ReturnABook(Guid bookRevisionId, Guid clientId);
    }
}