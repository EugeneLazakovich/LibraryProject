using System;
using System.Collections.Generic;
using System.Text;

namespace Lesson1_BL.DTOs
{
    public class RentBookDto
    {
        public BookDto BookDto { get; set; }
        public BookRevisionDto BookRevisionDto { get; set; }
        public LibraryDto LibraryDto { get; set; }
    }
}
