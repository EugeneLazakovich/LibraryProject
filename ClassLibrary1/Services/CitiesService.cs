using Lesson1_DAL;
using Lesson1_DAL.Interfaces;
using Lesson1_DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lesson1_BL
{
    public class CitiesService : ICitiesService
    {
        private readonly IGenericRepository<City> _citiesRepository;
        public CitiesService(IGenericRepository<City> citiesRepository)
        {
            _citiesRepository = citiesRepository;
        }
        public async Task<Guid> AddCity(City city)
        {
            return await _citiesRepository.Add(city);
        }

        public async Task<IEnumerable<City>> GetAllCities()
        {
            return await _citiesRepository.GetAll();
        }
    }
}
