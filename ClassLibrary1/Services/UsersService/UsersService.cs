using Lesson1_DAL;
using Lesson1_DAL.Interfaces;
using Lesson1_DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lesson1_BL.Services.UsersService
{
    public class UsersService : IUsersService
    {
        private readonly IGenericRepository<User> _clientsRepository;
        private readonly IGenericRepository<BookRevision> _booksRevisionRepository;
        private readonly DefaultSettings _defaultSettings = new DefaultSettings();
        public UsersService(IGenericRepository<User> clientsRepository, IGenericRepository<BookRevision> bookRevisionRepository)
        {
            _clientsRepository = clientsRepository;
            _booksRevisionRepository = bookRevisionRepository;
        }
        public async Task<Guid> AddClient(User client)
        {
            return await _clientsRepository.Add(client);
        }

        public async Task<bool> DeleteByIdClient(Guid id)
        {
            return await _clientsRepository.DeleteById(id);
        }

        public async Task<IEnumerable<User>> GetAllClients()
        {
            return await _clientsRepository.GetAll();
        }

        public async Task<User> GetByIdClient(Guid id)
        {
            return await _clientsRepository.GetById(id);
        }

        public async Task<bool> UpdateClient(User client)
        {
            return await _clientsRepository.Update(client);
        }

        public async Task<bool> RentABook(Guid bookRevisionId, Guid clientId)
        {
            /*var bookRevision = await _booksRevisionRepository.GetById(bookRevisionId);
            var client = await _clientsRepository.GetById(clientId);
            CheckEmptiesOnNull(bookRevision, client);
            CheckClientOnBlocked(client);
                        
            if (bookRevision.Client != null)
            {
                throw new ArgumentException("The book has been already rented!");
            }
            if (client.Books != null)
            {
                if (client.Books.Contains(bookRevision))
                {
                    throw new ArgumentException("The client has this book!");
                }                
            }
            bookRevision.Client = client;
            bookRevision.RentCount++;
            bookRevision.DateGet = DateTime.Now;
            bookRevision.DaysForRent = _defaultSettings.DaysForRent;
            await _booksRevisionRepository.Update(bookRevision);

            client.Books.Add(bookRevision);
            await _clientsRepository.Update(client);*/

            return true;
        }

        public async Task<bool> ReturnABook(Guid bookRevisionId, Guid clientId, bool isLost, bool isDamaged)
        {
            /*var bookRevision = await _booksRevisionRepository.GetById(bookRevisionId);
            var client = await _clientsRepository.GetById(clientId);
            CheckEmptiesOnNull(bookRevision, client);

            if (bookRevision.Client == null)
            {
                throw new ArgumentException("The book is in library!");
            }            
            if (client.Books == null)
            {
                if (!client.Books.Contains(bookRevision))
                {
                    throw new ArgumentException("The client don't have this book!");
                }                
            }
            bool result = false;
            CheckStateBook(client, bookRevision, isLost, isDamaged, ref result);
            if(bookRevision != null)
            {
                client.Books.Remove(bookRevision);
            }
            client.IsBlocked = client.Amount < 0;
            await _clientsRepository.Update(client);
            if (result)
            {
                throw new ArgumentException("The client lost this book!");
            }

            bookRevision.Client = null;
            bookRevision.DateOfRent = null;
            bookRevision.DaysForRent = 0;
            await _booksRevisionRepository.Update(bookRevision);  */         

            return true;
        }

        private void CheckStateBook(User client, BookRevision bookRevision, bool isLost, bool isDamaged, ref bool result)
        {
            /*if (isLost)
            {
                client.Amount -= _defaultSettings.PriceForLost;
                _booksRevisionRepository.DeleteById(bookRevision.Id);
                result = true;
            }
            else
            {
                if (isDamaged)
                {
                    client.Amount -= _defaultSettings.PriceForDamaged;
                }
                if ((DateTime.Now - (DateTime)bookRevision.DateOfRent).TotalDays > bookRevision.DaysForRent)
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

        private static void CheckEmptiesOnNull(BookRevision bookRevision, User client)
        {
            CheckBookOnNull(bookRevision);
            CheckClientOnNull(client); 
            CheckDateOfRentOnNull(bookRevision);
        }

        private static void CheckClientOnNull(User client)
        {
            if (client == null)
            {
                throw new ArgumentException("The client doesn't exist!");
            }
        }

        private static void CheckBookOnNull(BookRevision bookRevision)
        {
            if (bookRevision == null)
            {
                throw new ArgumentException("The book doesn't exist!");
            }
        }
        private static void CheckDateOfRentOnNull(BookRevision bookRevision)
        {
            /*if (bookRevision != null)
            {
                if(bookRevision.DateOfRent == null)
                {
                    throw new ArgumentException("The date of rent is empty!");
                }                
            }*/
        }

        private static void CheckClientOnBlocked(User client)
        {
            if (client != null && client.IsBlocked == true)
            {
                throw new ArgumentException("The client is blocked");
            }
        }
    }
}
