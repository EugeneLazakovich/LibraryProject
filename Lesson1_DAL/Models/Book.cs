using System.ComponentModel.DataAnnotations;

namespace Lesson1_DAL.Models
{
    public class Book : BaseEntity
    {
        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string Title { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Author { get; set; }
    }
}
