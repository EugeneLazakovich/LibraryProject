using Lesson1_DAL;
using Lesson1_DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lesson1_BL.Services.UsersService
{
    public interface IUsersService
    {
        Task<bool> RentABook(Guid bookId, Guid clientId);
        Task<bool> ReturnABook(Guid bookId, Guid clientId, bool isLost, bool isDamaged);
        Task<IEnumerable<User>> GetAllClients();
        Task<User> GetByIdClient(Guid id);
        Task<bool> DeleteByIdClient(Guid id);
        Task<bool> UpdateClient(User client);
        Task<Guid> AddClient(User client);
        Task<bool> Deposit(double amount, Guid clientId);
    }
}
