using Lesson1_DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lesson1_BL
{
    public interface ICitiesService
    {
        Task<IEnumerable<City>> GetAllCities();
        Task<Guid> AddCity(City city);
    }
}
