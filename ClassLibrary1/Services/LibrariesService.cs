using Lesson1_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lesson1_BL
{
    public class LibrariesService : ILibrariesService
    {
        private readonly ILibrariesRepository _librariesRepository;
        public LibrariesService(ILibrariesRepository librariesRepository)
        {
            _librariesRepository = librariesRepository;
        }
        public Guid AddLibrary(Library library)
        {
            return _librariesRepository.Add(library);
        }

        public IEnumerable<Library> GetAllLibraries()
        {
            return _librariesRepository.GetAll();
        }

        public IEnumerable<Library> GetNearestLibraries(Location location, int top)
        {
            Dictionary<Library, double> unsortedDict = new Dictionary<Library, double>();
            var libraries = _librariesRepository.GetAll();
            double XCoor = location.XCoordinate;
            double YCoor = location.YCoordinate;
            foreach(var library in libraries.Where(c => c.Location != null))
            {
                library.Location.XCoordinate -= XCoor;
                if(library.Location.XCoordinate < -180)
                {
                    library.Location.XCoordinate += 360;
                }
                library.Location.YCoordinate -= YCoor;
                if (library.Location.YCoordinate < -180)
                {
                    library.Location.YCoordinate += 360;
                }
                double distance = Math.Sqrt(Math.Pow(library.Location.XCoordinate, 2) + Math.Pow(library.Location.YCoordinate, 2));
                unsortedDict.Add(library, distance);
            }
            //var sortedDict = from entry in dictUnsortableLibraries orderby entry.Value ascending select entry;
            var sortedDict = unsortedDict.OrderBy(c => c.Value);

            return sortedDict.Select(c => c.Key).Take(top);
        }
    }
}
