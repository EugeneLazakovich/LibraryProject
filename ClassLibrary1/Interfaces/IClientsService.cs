using Lesson1_DAL;
using Lesson1_DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lesson1_BL
{
    public interface IClientsService
    {
        Task<bool> RentABook(Guid bookId, Guid clientId);
        Task<bool> ReturnABook(Guid bookId, Guid clientId, bool isLost, bool isDamaged);
        Task<IEnumerable<Client>> GetAllClients();
        Task<Client> GetByIdClient(Guid id);
        Task<bool> DeleteByIdClient(Guid id);
        Task<bool> UpdateClient(Client client);
        Task<Guid> AddClient(Client client);
        Task<bool> Deposit(double amount, Guid clientId);
    }
}
