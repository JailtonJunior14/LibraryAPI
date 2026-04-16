using Library.Data.Entities;
using Library.Data.Persistence;
using Library.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Library.Data.Seeds
{
    public class UserSeed
    {
        public static async Task Seed(LibraryDbContext context)
        {
            if (await context.User.AnyAsync())
                return;

            var hasher = new PasswordHasher<User>();

            var users = new List<User>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Role = UserRole.Admin,
                UserName = "Admin1",
                Email = "admin@admin.com"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Role = UserRole.librarian,
                UserName = "librarian1",
                Email = "librarian@lib.com"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Role = UserRole.student,
                UserName = "student1",
                Email = "student@student.com"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Role = UserRole.student,
                UserName = "student2",
                Email = "student2@student.com"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Role = UserRole.student,
                UserName = "student3",
                Email = "student3@student.com"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Role = UserRole.student,
                UserName = "student4",
                Email = "student4@student.com"
            }
        };

            foreach (var user in users)
                user.PasswordHash = hasher.HashPassword(user, "123456");

            context.User.AddRange(users);
            await context.SaveChangesAsync();
        }
    }
}
