using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lesson1_DAL.Models
{
    public class RentBook : BaseEntity
    {
        [ForeignKey("LibraryBook")]
        public Guid LibraryBookId { get; set; }
        public LibraryBooks LibraryBook { get; set; }
        [ForeignKey("Client")]
        public Guid ClientId { get; set; }
        public User Client { get; set; }
        public DateTime DateGet { get; set; }
        public DateTime? DateReturn { get; set; }
    }
}
