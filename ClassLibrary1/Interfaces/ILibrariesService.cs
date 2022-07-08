using Lesson1_DAL;
using Lesson1_DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lesson1_BL
{
    public interface ILibrariesService
    {
        Task<IEnumerable<Library>> GetAllLibraries();
        Task<Guid> AddLibrary(Library library);
        Task<IEnumerable<Library>> GetNearestLibraries(Location location, int top);
    }
}
