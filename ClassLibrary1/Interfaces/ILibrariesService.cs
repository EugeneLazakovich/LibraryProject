using Lesson1_DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lesson1_BL
{
    public interface ILibrariesService
    {
        IEnumerable<Library> GetAllLibraries();
        Guid AddLibrary(Library library);
        IEnumerable<Library> GetNearestLibraries(Location location, int top);
    }
}
