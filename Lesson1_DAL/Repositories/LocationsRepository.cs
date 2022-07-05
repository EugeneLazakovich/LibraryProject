using System;
using System.Collections.Generic;
using System.Linq;

namespace Lesson1_DAL
{
    public class LocationsRepository : ILocationsRepository
    {
        private readonly EFCoreDbContext _dbContext;
        public LocationsRepository(EFCoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Guid Add(Location location)
        {
            location.Id = Guid.NewGuid();
            _dbContext.Add(location);
            _dbContext.SaveChanges();

            return location.Id;
        }

        public IEnumerable<Location> GetAll()
        {
            return _dbContext.Locations.ToList();
        }
    }
}
