using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lesson1_DAL.Models
{
    public class BookRevision : BaseEntity
    {
        [ForeignKey("Book")]
        public Guid BookId { get; set; }
        public Book Book { get; set; }
        public int YearOfPublishing { get; set; }
        public int PagesCount { get; set; }
        public float Price { get; set; }
    }
}
