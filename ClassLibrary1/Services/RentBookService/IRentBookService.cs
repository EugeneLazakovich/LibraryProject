using Lesson1_BL.DTOs;
using Lesson1_DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lesson1_BL.Services.RentBookService
{
    public interface IRentBookService
    {
        Task<RentBookDto> RentABook(Guid bookId, Guid clientId, Guid libraryId);
        Task<bool> ReturnABook(Guid bookRevisionId, Guid clientId);
    }
}