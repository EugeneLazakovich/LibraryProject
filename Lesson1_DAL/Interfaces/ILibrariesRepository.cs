using System;
using System.Collections.Generic;
using System.Text;

namespace Lesson1_DAL
{
    public interface ILibrariesRepository
    {
        IEnumerable<Library> GetAll();
        Guid Add(Library library);
    }
}
