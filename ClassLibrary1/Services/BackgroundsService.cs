using Lesson1_DAL;
using Lesson1_DAL.Interfaces;
using Lesson1_DAL.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Lesson1_BL
{
    public class BackgroundsService : IBackgroundsService
    {
        private readonly IGenericRepository<User> _clientsRepository;
        private readonly DefaultSettings _defaultSettings = new DefaultSettings();
        public BackgroundsService(IGenericRepository<User> clientsRepository)
        {
            _clientsRepository = clientsRepository;
        }
        public async Task<bool> PayPerMonth()
        {
            if (DateTime.Now.Day == 1)
            {
                var clients = (await _clientsRepository.GetAll()).Where(c => c.IsBlocked == false);
                foreach(var client in clients)
                {
                    client.Amount -= _defaultSettings.PricePerMonth;
                    client.IsBlocked = client.Amount < 0;
                    await _clientsRepository.Update(client);
                }
            }

            return true;
        }
    }
}
