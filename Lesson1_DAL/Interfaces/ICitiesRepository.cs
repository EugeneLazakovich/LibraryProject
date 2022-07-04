using System;
using System.Collections.Generic;
using System.Text;

namespace Lesson1_DAL
{
    public interface ICitiesRepository
    {
        IEnumerable<City> GetAll();
        Guid Add(City city);
    }
}
