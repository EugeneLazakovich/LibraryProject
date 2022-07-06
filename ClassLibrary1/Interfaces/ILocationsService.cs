using Lesson1_DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lesson1_BL
{
    public interface ILocationsService
    {
        IEnumerable<Location> GetAllLocations();
        Guid AddLocation(Location location);
    }
}
