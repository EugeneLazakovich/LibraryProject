using Lesson1_DAL;
using System.Collections.Generic;

namespace Lesson1_BL
{
    public class LocationsService : ILocationsService
    {
        private readonly ILocationsRepository _locationsRepository;
        public LocationsService(ILocationsRepository locationsRepository)
        {
            _locationsRepository = locationsRepository;
        }
        /*public Guid AddLocation(Location location)
        {
            return _locationsRepository.Add(location);
        }*/

        public IEnumerable<Location> GetAllLocations()
        {
            return _locationsRepository.GetAll();
        }
    }
}
