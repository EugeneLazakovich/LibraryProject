using System;
using System.Collections.Generic;
using System.Text;

namespace Lesson1_BL.DTOs
{
    public class UserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public bool IsBlocked { get; set; }
        public double Amount { get; set; }
        public string Password { get; set; }
    }
}
