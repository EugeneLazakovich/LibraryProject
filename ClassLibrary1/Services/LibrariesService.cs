using Lesson1_DAL;
using Lesson1_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lesson1_BL
{
    public class LibrariesService : ILibrariesService
    {
        private readonly IGenericRepository<Library> _librariesRepository;
        public LibrariesService(IGenericRepository<Library> librariesRepository)
        {
            _librariesRepository = librariesRepository;
        }
        public async Task<Guid> AddLibrary(Library library)
        {
            return await _librariesRepository.Add(library);
        }

        public async Task<IEnumerable<Library>> GetAllLibraries()
        {
            return await _librariesRepository.GetAll();
        }

        public async Task<IEnumerable<Library>> GetNearestLibraries(Location location, int top)
        {
            Dictionary<Library, double> unsortedDict = new Dictionary<Library, double>();
            var libraries = await _librariesRepository.GetAll();
            double XCoor = location.XCoordinate;
            double YCoor = location.YCoordinate;
            /*foreach(var library in libraries.Where(c => c.Location != null))
            {
                library.Location.XCoordinate -= XCoor;
                if(library.Location.XCoordinate < -180)
                {
                    library.Location.XCoordinate += 360;
                }
                library.Location.YCoordinate -= YCoor;
                if (library.Location.YCoordinate < -90)
                {
                    library.Location.YCoordinate += 90 - library.Location.YCoordinate;
                }
                double distance = Math.Sqrt(Math.Pow(library.Location.XCoordinate, 2) + Math.Pow(library.Location.YCoordinate, 2));
                unsortedDict.Add(library, distance);
            }*/
            //var sortedDict = from entry in dictUnsortableLibraries orderby entry.Value ascending select entry;
            var sortedDict = unsortedDict.OrderBy(c => c.Value);

            return sortedDict.Select(c => c.Key).Take(top);
        }
    }
}
