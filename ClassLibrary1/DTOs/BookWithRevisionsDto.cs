using System;
using System.Collections.Generic;

namespace Lesson1_BL.DTOs
{
    public class BookWithRevisionsDto
    {
        public Guid BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public IEnumerable<BookRevisionDto> BookRevisions { get; set; }

    }
}
