using System;
using System.Collections.Generic;

namespace Lesson1_DAL
{
    public interface ILocationsRepository
    {
        IEnumerable<Location> GetAll();
        Guid Add(Location location);
    }
}
