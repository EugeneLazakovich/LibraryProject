using System;
using System.Collections.Generic;
using System.Linq;

namespace Lesson1_DAL
{
    public class LibrariesRepository : ILibrariesRepository
    {
        private readonly EFCoreDbContext _dbContext;
        public LibrariesRepository(EFCoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Guid Add(Library library)
        {
            library.Id = Guid.NewGuid();
            library.Books = new List<Book>();
            Location location = new Location();
            GetRandomLocation(location);
            location.Library = library;
            library.Location = location;

            library.City = GetNewCity();
            if(library.City.Libraries == null)
            {
                library.City.Libraries = new List<Library>
                {
                    library
                };
            }
            else
            {
                library.City.Libraries.Add(library);
            }


            _dbContext.Add(library);
            _dbContext.Add(location);
            _dbContext.SaveChanges();

            return library.Id;
        }

        private static void GetRandomLocation(Location location)
        {
            Random random = new Random();
            location.Id = Guid.NewGuid();
            location.XCoordinate = random.NextDouble() * (180 - (-179.99)) + (-179.99);
            location.YCoordinate = random.NextDouble() * (180 - (-179.99)) + (-179.99);
        }

        private City GetNewCity()
        {            
            var countCities = _dbContext.Cities.ToList().Count;
            if(countCities == 0)
            {
                City cityNew = new City
                {
                    Id = Guid.NewGuid(),
                    Name = "FirstCity",
                    Libraries = new List<Library>()
                };
                _dbContext.Add(cityNew);

                return cityNew;
            }
            Random random = new Random();
            int range = random.Next(0, countCities);
            City city = _dbContext.Cities.ToList()[range];

            return city;
        }

        public IEnumerable<Library> GetAll()
        {
            return _dbContext.Libraries.ToList();
        }
    }
}
