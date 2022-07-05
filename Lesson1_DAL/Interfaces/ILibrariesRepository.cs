using System;
using System.Collections.Generic;

namespace Lesson1_DAL
{
    public interface ILibrariesRepository
    {
        IEnumerable<Library> GetAll();
        Guid Add(Library library);
    }
}
