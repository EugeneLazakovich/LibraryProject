using System;
using System.Collections.Generic;

namespace Lesson1_DAL
{
    public interface ICitiesRepository
    {
        IEnumerable<City> GetAll();
        Guid Add(City city);
    }
}
