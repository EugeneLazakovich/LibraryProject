using System;
using System.Collections.Generic;
using System.Text;

namespace Lesson1_DAL
{
    public interface IClientsRepository
    {
        IEnumerable<Client> GetAll();
        Client GetById(Guid id);
        bool DeleteById(Guid id);
        bool Update(Client client);
        Guid Add(Client client);
    }
}
