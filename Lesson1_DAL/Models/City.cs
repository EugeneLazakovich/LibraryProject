using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lesson1_DAL.Models
{
    public class City : BaseEntity
    {
        [Required]
        [MinLength(2)]
        [MaxLength(30)]
        public string Name { get; set; }
    }
}
