using Lesson1_DAL;
using Lesson1_DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lesson1_BL
{
    public class ClientsService : IClientsService
    {
        private readonly IGenericRepository<Client> _clientsRepository;
        private readonly IGenericRepository<Book> _booksRepository;
        private readonly DefaultSettings _defaultSettings = new DefaultSettings();
        public ClientsService(IGenericRepository<Client> clientsRepository, IGenericRepository<Book> booksRepository)
        {
            _clientsRepository = clientsRepository;
            _booksRepository = booksRepository;
        }
        public async Task<Guid> AddClient(Client client)
        {
            return await _clientsRepository.Add(client);
        }

        public async Task<bool> DeleteByIdClient(Guid id)
        {
            return await _clientsRepository.DeleteById(id);
        }

        public async Task<IEnumerable<Client>> GetAllClients()
        {
            return await _clientsRepository.GetAll();
        }

        public async Task<Client> GetByIdClient(Guid id)
        {
            return await _clientsRepository.GetById(id);
        }

        public async Task<bool> UpdateClient(Client client)
        {
            return await _clientsRepository.Update(client);
        }

        public async Task<bool> RentABook(Guid bookId, Guid clientId)
        {
            var book = await _booksRepository.GetById(bookId);
            var client = await _clientsRepository.GetById(clientId);
            CheckEmptiesOnNull(book, client);
            CheckClientOnBlocked(client);
                        
            /*if (book.Client != null)
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
            book.DaysForRent = _defaultSettings.DaysForRent;*/
            await _booksRepository.Update(book);

            client.Books.Add(book);
            await _clientsRepository.Update(client);

            return true;
        }

        public async Task<bool> ReturnABook(Guid bookId, Guid clientId, bool isLost, bool isDamaged)
        {
            var  book = await _booksRepository.GetById(bookId);
            var client = await _clientsRepository.GetById(clientId);
            CheckEmptiesOnNull(book, client);

            /*if (book.Client == null)
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
            client.IsBlocked = client.Amount < 0;
            await _clientsRepository.Update(client);
            if (result)
            {
                throw new ArgumentException("The client lost this book!");
            }

            book.Client = null;
            book.DateOfRent = null;
            book.DaysForRent = 0;*/
            await _booksRepository.Update(book);           

            return true;
        }

        private void CheckStateBook(Client client, Book book, bool isLost, bool isDamaged, ref bool result)
        {
            /*if (isLost)
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
            }  */          
        }

        public async Task<bool> Deposit(double amount, Guid clientId)
        {
            var client = await _clientsRepository.GetById(clientId);
            CheckClientOnNull(client);
            client.Amount += amount;
            await _clientsRepository.Update(client);

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
            /*if (book != null)
            {
                if(book.DateOfRent == null)
                {
                    throw new ArgumentException("The date of rent is empty!");
                }                
            }*/
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
