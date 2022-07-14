using System;
using System.ComponentModel.DataAnnotations;

namespace Lesson1_DAL.Models
{
    public abstract class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
    }
}
