using Lesson1_DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lesson1_BL
{
    public interface IClientsService
    {
        bool RentABook(Guid bookId, Guid clientId);
        bool ReturnABook(Guid bookId, Guid clientId);
        IEnumerable<Client> GetAllClients();
        Client GetByIdClient(Guid id);
        bool DeleteByIdClient(Guid id);
        bool UpdateClient(Client client);
        Guid AddClient(Client client);
        bool Deposit(double amount, Guid clientId);
    }
}
