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
        private readonly DefaultSettings _defaultSettings = new DefaultSettings();
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

        public bool UpdateClient(Client client)
        {
            return _clientsRepository.Update(client);
        }

        public bool RentABook(Guid bookId, Guid clientId)
        {
            var book = _booksRepository.GetById(bookId);
            var client = _clientsRepository.GetById(clientId);
            CheckEmptiesOnNull(book, client);
            CheckClientOnBlocked(client);
                        
            if (book.Client != null)
            {
                throw new ArgumentException("The book has been already rented!");
            }
            if (client.Books != null)
            {
                if (client.Books.Contains(book))
                {
                    throw new ArgumentException("The client has this book!");
                }                
            }
            book.Client = client;
            book.RentCount++;
            book.DateOfRent = DateTime.Now;
            book.DaysForRent = _defaultSettings.DaysForRent;
            _booksRepository.Update(book);

            client.Books.Add(book);
            _clientsRepository.Update(client);

            return true;
        }

        public bool ReturnABook(Guid bookId, Guid clientId, bool isLost, bool isDamaged)
        {
            var  book = _booksRepository.GetById(bookId);
            var client = _clientsRepository.GetById(clientId);
            CheckEmptiesOnNull(book, client);

            if (book.Client == null)
            {
                throw new ArgumentException("The book is in library!");
            }            
            if (client.Books == null)
            {
                if (!client.Books.Contains(book))
                {
                    throw new ArgumentException("The client don't have this book!");
                }                
            }
            bool result = false;
            CheckStateBook(client, book, isLost, isDamaged, ref result);
            if(book != null)
            {
                client.Books.Remove(book);
            }            
            client.IsBlocked = client.Amount < 0 ? true : false;
            _clientsRepository.Update(client);
            if (result)
            {
                throw new ArgumentException("The client lost this book!");
            }

            book.Client = null;
            book.DateOfRent = null;
            book.DaysForRent = 0;
            _booksRepository.Update(book);           

            return true;
        }

        private void CheckStateBook(Client client, Book book, bool isLost, bool isDamaged, ref bool result)
        {
            if (isLost)
            {
                client.Amount -= _defaultSettings.PriceForLost;
                _booksRepository.DeleteById(book.Id);
                result = true;
            }
            else
            {
                if (isDamaged)
                {
                    client.Amount -= _defaultSettings.PriceForDamaged;
                }
                if ((DateTime.Now - (DateTime)book.DateOfRent).TotalDays > book.DaysForRent)
                {
                    client.Amount -= _defaultSettings.PriceForDelayed;
                }
            }            
        }

        public bool Deposit(double amount, Guid clientId)
        {
            var client = _clientsRepository.GetById(clientId);
            CheckClientOnNull(client);
            client.Amount += amount;
            _clientsRepository.Update(client);

            return true;
        }

        private static void CheckEmptiesOnNull(Book book, Client client)
        {
            CheckBookOnNull(book);
            CheckClientOnNull(client); 
            CheckDateOfRentOnNull(book);
        }

        private static void CheckClientOnNull(Client client)
        {
            if (client == null)
            {
                throw new ArgumentException("The client doesn't exist!");
            }
        }

        private static void CheckBookOnNull(Book book)
        {
            if (book == null)
            {
                throw new ArgumentException("The book doesn't exist!");
            }
        }
        private static void CheckDateOfRentOnNull(Book book)
        {
            if (book != null)
            {
                if(book.DateOfRent == null)
                {
                    throw new ArgumentException("The date of rent is empty!");
                }                
            }
        }

        private static void CheckClientOnBlocked(Client client)
        {
            if (client != null && client.IsBlocked == true)
            {
                throw new ArgumentException("The client is blocked");
            }
        }
    }
}
