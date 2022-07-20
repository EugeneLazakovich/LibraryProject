using Lesson1_DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lesson1_DAL.Interfaces
{
    public interface ILibrariesRepository
    {
        Task<IEnumerable<Library>> GetNearestLibraries(Location userLocation, int top);
    }
}
