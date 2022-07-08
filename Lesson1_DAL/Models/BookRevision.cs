using System;
using System.Collections.Generic;
using System.Text;

namespace Lesson1_DAL.Models
{
    public class BookRevision : BaseEntity
    {
        public Guid BookId { get; set; }
        public Book Book { get; set; }
        public int YearOfPublishing { get; set; }
        public int PagesCount { get; set; }
        public float Price { get; set; }
    }
}
