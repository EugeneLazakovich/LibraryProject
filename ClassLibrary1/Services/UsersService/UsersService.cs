using Lesson1_BL.Services.LibrariesService;
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
        private readonly IRentBookRepository _rentBookRepository;
        private readonly ILibrariesService _librariesService;
        private readonly DefaultSettings _defaultSettings = new DefaultSettings();
        public UsersService(
            IGenericRepository<User> clientsRepository, 
            IGenericRepository<BookRevision> bookRevisionRepository, 
            IRentBookRepository rentBookRepository,
            ILibrariesService librariesService)
        {
            _clientsRepository = clientsRepository;
            _booksRevisionRepository = bookRevisionRepository;
            _rentBookRepository = rentBookRepository;
            _librariesService = librariesService;
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
