using System;
using System.ComponentModel.DataAnnotations;

namespace Lesson1_DAL
{
    public class Book
    {
        public Guid Id { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string Title { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Author { get; set; }
        [Required]
        public int PagesCount { get; set; }
        public Library Library { get; set; }
        public Client Client { get; set; }
        public bool IsRent { get; set; }
        public int RentCount { get; set; }
    }
}
