using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lesson1_DAL
{
    public class CitiesRepository : ICitiesRepository
    {
        private readonly EFCoreDbContext _dbContext;
        public CitiesRepository(EFCoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Guid Add(City city)
        {
            city.Id = Guid.NewGuid();
            city.Libraries = new List<Library>();
            _dbContext.Add(city);
            _dbContext.SaveChanges();

            return city.Id;
        }

        public IEnumerable<City> GetAll()
        {
            return _dbContext.Cities.ToList();
        }
    }
}
