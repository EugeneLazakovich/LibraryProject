using Lesson1_DAL.Interfaces;
using Lesson1_DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lesson1_DAL.Repositories
{
    public class LibrariesRepository : ILibrariesRepository
    {
        private readonly EFCoreDbContext _dbContext;
        public LibrariesRepository(EFCoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Library>> GetNearestLibraries(Location userLocation, int top)
        {
            return await _dbContext.Libraries
                .Include(l => l.City)
                .Include(l => l.Location)
                .OrderBy(l => 2 * 6371
                    *
                    Math.Asin(
                        Math.Sqrt(
                            Math.Pow(Math.Sin(((Math.PI / 180) * (l.Location.XCoordinate) - (Math.PI / 180) * (userLocation.XCoordinate)) / 2), 2)
                            +
                            Math.Cos((Math.PI / 180) * (userLocation.XCoordinate))
                            *
                            Math.Cos((Math.PI / 180) * (l.Location.XCoordinate))
                            *
                            Math.Pow(Math.Sin(((Math.PI / 180) * (l.Location.YCoordinate) - (Math.PI / 180) * (userLocation.YCoordinate)) / 2), 2)
                        )
                    )
                )
                .Take(top)
                .ToListAsync();
        }        
    }
}
