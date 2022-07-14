using Lesson1_DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

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
