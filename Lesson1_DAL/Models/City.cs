using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Lesson1_DAL
{
    public class City
    {
        public Guid Id { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(30)]
        public string Name { get; set; }
        public List<Library> Libraries { get; set; }
    }
}
