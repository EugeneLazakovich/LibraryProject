using Lesson1_DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lesson1_BL
{
    public class CitiesService : ICitiesService
    {
        private readonly ICitiesRepository _citiesRepository;
        public CitiesService(ICitiesRepository citiesRepository)
        {
            _citiesRepository = citiesRepository;
        }
        public Guid AddCity(City city)
        {
            return _citiesRepository.Add(city);
        }

        public IEnumerable<City> GetAllCities()
        {
            return _citiesRepository.GetAll();
        }
    }
}
