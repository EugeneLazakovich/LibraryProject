using Lesson1_BL.DTOs;
using Lesson1_DAL;
using Lesson1_DAL.Interfaces;
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
            float XCoor = location.XCoordinate;
            float YCoor = location.YCoordinate;
            foreach(var library in libraries.Where(c => c.Location != null))
            {
                var libX = library.Location.XCoordinate;
                var libY = library.Location.YCoordinate;
                libX -= XCoor;
                if(libX < -180)
                {
                    libX += 360;
                }
                if (libX > 180)
                {
                    libX -= 360;
                }
                libY -= YCoor;
                if (libY < -90)
                {
                    libY = -180 - libY;
                }
                if (libY > 90)
                {
                    libY = 180 - libY;
                }
                double distance = Math.Sqrt(Math.Pow(libX, 2) + Math.Pow(libY, 2));
                unsortedDict.Add(library, distance);
            }
            //var sortedDict = from entry in dictUnsortableLibraries orderby entry.Value ascending select entry;
            var sortedDict = unsortedDict.OrderBy(c => c.Value);

            return sortedDict.Select(c => c.Key).Take(top);
        }
    }
}
