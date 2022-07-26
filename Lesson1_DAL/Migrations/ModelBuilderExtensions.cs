using Lesson1_DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Lesson1_DAL.Migrations
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = Guid.NewGuid(), Name = "Librarian" },
                new Role { Id = Guid.NewGuid(), Name = "Reader" },
                new Role { Id = Guid.NewGuid(), Name = "Admin" });
        }
    }
}
