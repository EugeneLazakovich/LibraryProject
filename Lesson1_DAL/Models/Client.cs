using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Lesson1_DAL
{
    public class Client
    {
        public Guid Id { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(20)]
        public string FirstName { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(20)]
        public string LastName { get; set; }
        public List<Book> Books { get; set; }
    }
}
