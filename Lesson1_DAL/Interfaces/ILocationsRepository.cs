using System;
using System.Collections.Generic;
using System.Text;

namespace Lesson1_DAL
{
    public interface ILocationsRepository
    {
        IEnumerable<Location> GetAll();
        Guid Add(Location location);
    }
}
