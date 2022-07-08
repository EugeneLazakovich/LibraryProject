using System;
using System.Collections.Generic;
using System.Text;

namespace Lesson1_DAL.Models
{
    public class RentBook : BaseEntity
    {
        public Guid LibraryBookId { get; set; }
        public Guid ClientId { get; set; }
        public DateTime DateGet { get; set; }
        public DateTime? DateReturn { get; set; }
    }
}
