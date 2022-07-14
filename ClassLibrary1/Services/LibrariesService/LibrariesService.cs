using Lesson1_BL.DTOs;
using Lesson1_DAL.Interfaces;
using Lesson1_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lesson1_BL.Services.LibrariesService
{
    public class LibrariesService : ILibrariesService
    {
        private readonly IGenericRepository<Library> _genericLibrariesRepository;
        private readonly ILibrariesRepository _librariesRepository;
        public LibrariesService(IGenericRepository<Library> genericLibrariesRepository, ILibrariesRepository librariesRepository)
        {
            _genericLibrariesRepository = genericLibrariesRepository;
            _librariesRepository = librariesRepository;
        }
        public async Task<IEnumerable<NearestLibraryDto>> GetNearestLibraries(Location location, int top = 10)
        {
            var libraries = await _librariesRepository
                .GetNearestLibraries(location, top);
                //.Select(l => new Tuple<Library, double> { l, CalculateDistance(l, location) });
            var tupleList = new List<Tuple<Library, double>>();
            foreach(var library in libraries)
            {
                tupleList.Add(new Tuple<Library, double>(library, CalculateDistance(library, location)));
            }

            return MapFromTupleToNearestLibraryDto(tupleList);
        }

        private double CalculateDistance(Library library, Location userLocation)
        {
            var distance = 2 * 6371
                *
                Math.Asin(
                    Math.Sqrt(
                        Math.Pow(Math.Sin((ConvertToRadians(library.Location.XCoordinate) - ConvertToRadians(userLocation.XCoordinate)) / 2), 2)
                        +
                        Math.Cos(ConvertToRadians(userLocation.XCoordinate))
                        *
                        Math.Cos(ConvertToRadians(library.Location.XCoordinate))
                        *
                        Math.Pow(Math.Sin((ConvertToRadians(library.Location.YCoordinate) - ConvertToRadians(userLocation.YCoordinate)) / 2), 2)
                    )
                );

            return distance;
        }

        private double ConvertToRadians(float angle)
        {
            return (Math.PI / 180) * angle;
        }

        private IEnumerable<NearestLibraryDto> MapFromTupleToNearestLibraryDto(List<Tuple<Library, double>> result)
        {
            return result.Select(c => new NearestLibraryDto
            {
                XCoordinate = c.Item1.Location.XCoordinate,
                YCoordinate = c.Item1.Location.YCoordinate,
                CityName = c.Item1.City.Name,
                FullAddress = c.Item1.FullAddress,
                Distance = c.Item2
            });
        }
    }
}
