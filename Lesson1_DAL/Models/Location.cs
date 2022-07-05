using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lesson1_DAL
{
    public class Location
    {
        [ForeignKey("Library")]
        public Guid Id { get; set; }
        [Required]
        [Range(-179.99, 180)]
        public double XCoordinate { get; set; }
        [Required]
        [Range(-179.99, 180)]
        public double YCoordinate { get; set; }
        public Library Library { get; set; }
    }
}
