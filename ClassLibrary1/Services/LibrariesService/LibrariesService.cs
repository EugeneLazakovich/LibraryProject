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
        private readonly ILibrariesRepository _librariesRepository;
        public LibrariesService(ILibrariesRepository librariesRepository)
        {
            _librariesRepository = librariesRepository;
        }
        public async Task<IEnumerable<NearestLibraryDto>> GetNearestLibraries(Location location, int top = 10)
        {
            return (await _librariesRepository
                .GetNearestLibraries(location, top))
                .Select(l => new NearestLibraryDto
                {
                    XCoordinate = l.Location.XCoordinate,
                    YCoordinate = l.Location.YCoordinate,
                    CityName = l.City.Name,
                    FullAddress = l.FullAddress,
                    Distance = CalculateDistance(l, location)
                });
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
    }
}
