using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lesson1_DAL.Models
{
    public class Library : BaseEntity
    {
        public string FullAddress { get; set; }
        [ForeignKey("Location")]
        public Guid LocationId { get; set; }
        [ForeignKey("City")]
        public Guid CityId { get; set; }
        public City City { get; set; }
        public Location Location { get; set; }
    }
}
