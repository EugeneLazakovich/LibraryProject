using System;
using System.ComponentModel.DataAnnotations;

namespace Lesson1_BL.DTOs
{
    public class UserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        [RegularExpression(@"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))"
                +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$")]
        public string Email { get; set; }
        public bool IsBlocked { get; set; }
        public double Amount { get; set; }
        public string Password { get; set; }
    }
}
