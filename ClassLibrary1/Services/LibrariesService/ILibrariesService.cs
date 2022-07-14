using Lesson1_BL.DTOs;
using Lesson1_DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lesson1_BL.Services.LibrariesService
{
    public interface ILibrariesService
    {
        Task<IEnumerable<NearestLibraryDto>> GetNearestLibraries(Location location, int top);
    }
}
