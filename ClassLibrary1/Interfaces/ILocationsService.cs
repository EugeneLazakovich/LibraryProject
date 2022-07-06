using Lesson1_DAL;
using System.Collections.Generic;

namespace Lesson1_BL
{
    public interface ILocationsService
    {
        IEnumerable<Location> GetAllLocations();
        //Guid AddLocation(Location location);
    }
}
