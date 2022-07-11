using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lesson1_DAL.Models
{
    public class Client : BaseEntity
    {
        [Required]
        [MinLength(2)]
        [MaxLength(20)]
        public string FirstName { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(20)]
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public bool IsBlocked { get; set; }
        public List<Book> Books { get; set; }
        public double Amount { get; set; }
    }
}
