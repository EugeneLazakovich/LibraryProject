using System;
using System.ComponentModel.DataAnnotations;

namespace Lesson1_DAL.Models
{
    public class Location : BaseEntity
    {
        [Required]
        [Range(-180, 180)]
        public float XCoordinate { get; set; }
        [Required]
        [Range(-90, 90)]
        public float YCoordinate { get; set; }
    }
}
