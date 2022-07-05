using Lesson1_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lesson1_BL
{
    public class BackgroundsService : IBackgroundsService
    {
        private readonly IClientsRepository _clientsRepository;
        private readonly DefaultSettings _defaultSettings = new DefaultSettings();
        public BackgroundsService(IClientsRepository clientsRepository)
        {
            _clientsRepository = clientsRepository;
        }
        public void PayPerMonth()
        {
            if (DateTime.Now.Day == 1)
            {
                var clients = _clientsRepository.GetAll().Where(c => c.IsBlocked == false);
                foreach(var client in clients)
                {
                    client.Amount -= _defaultSettings.PricePerMonth;
                    client.IsBlocked = client.Amount < 0 ? true : false;
                    _clientsRepository.Update(client);
                }
            }
        }
    }
}
