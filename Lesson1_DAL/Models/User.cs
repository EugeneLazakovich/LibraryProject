using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lesson1_DAL.Models
{
    public class User : BaseEntity
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
        public string Email { get; set; }
        public bool IsConfirmed { get; set; }
        public bool IsBlocked { get; set; }
        public double Amount { get; set; }
        public string Password { get; set; }
        [ForeignKey("Role")]
        public Guid? RoleId { get; set; }
        public Role Role { get; set; }
    }
}
