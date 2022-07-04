using Lesson1_DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lesson1_BL
{
    public class ClientsService : IClientsService
    {
        private readonly IClientsRepository _clientsRepository;
        private readonly IBooksRepository _booksRepository;
        public ClientsService(IClientsRepository clientsRepository, IBooksRepository booksRepository)
        {
            _clientsRepository = clientsRepository;
            _booksRepository = booksRepository;
        }
        public Guid AddClient(Client client)
        {
            return _clientsRepository.Add(client);
        }

        public bool DeleteByIdClient(Guid id)
        {
            return _clientsRepository.DeleteById(id);
        }

        public IEnumerable<Client> GetAllClients()
        {
            return _clientsRepository.GetAll();
        }

        public Client GetByIdClient(Guid id)
        {
            return _clientsRepository.GetById(id);
        }

        public bool RentABook(Guid bookId, Guid clientId)
        {
            var book = _booksRepository.GetById(bookId);
            if (book == null)
            {
                throw new ArgumentException("The book doesn't exist!");
            }

            var client = _clientsRepository.GetById(clientId);
            if (client == null)
            {
                throw new ArgumentException("The client doesn't exist!");
            }

            if (book.IsRent == true)
            {
                throw new ArgumentException("The book has been already rented!");
            }
            if (client.Books.Contains(book))
            {
                throw new ArgumentException("The client has this book!");
            }
            book.IsRent = true;
            book.RentCount++;
            _booksRepository.Update(book);

            client.Books.Add(book);
            _clientsRepository.Update(client);

            return true;
        }

        public bool ReturnABook(Guid bookId, Guid clientId)
        {
            var book = _booksRepository.GetById(bookId);
            if (book == null)
            {
                throw new ArgumentException("The book doesn't exist!");
            }

            var client = _clientsRepository.GetById(clientId);
            if (client == null)
            {
                throw new ArgumentException("The client doesn't exist!");
            }

            if (book.IsRent == false)
            {
                throw new ArgumentException("The book is in library!");
            }            
            if (!client.Books.Contains(book))
            {
                throw new ArgumentException("The client don't have this book!");
            }
            book.IsRent = false;
            _booksRepository.Update(book);

            client.Books.Remove(book);
            _clientsRepository.Update(client);

            return true;
        }

        public bool UpdateClient(Client client)
        {
            return _clientsRepository.Update(client);
        }
    }
}
