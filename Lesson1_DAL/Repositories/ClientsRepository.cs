using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lesson1_DAL
{
    public class ClientsRepository : IClientsRepository
    {
        private readonly EFCoreDbContext _dbContext;
        public ClientsRepository(EFCoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Guid Add(Client client)
        {
            client.Id = Guid.NewGuid();
            client.IsBlocked = false;
            client.Books = new List<Book>();
            _dbContext.Clients.Add(client);
            _dbContext.SaveChanges();

            return client.Id;
        }

        public bool DeleteById(Guid id)
        {
            var client = new Client { Id = id };
            _dbContext.Entry(client).State = EntityState.Deleted;

            return _dbContext.SaveChanges() != 0;
        }

        public IEnumerable<Client> GetAll()
        {
            return _dbContext.Clients.ToList();
        }

        public Client GetById(Guid id)
        {
            return _dbContext.Clients.Where(c => c.Id == id).FirstOrDefault();
        }

        public bool Update(Client client)
        {
            _dbContext.Entry(client).State = EntityState.Modified;

            return _dbContext.SaveChanges() != 0;
        }
    }
}
