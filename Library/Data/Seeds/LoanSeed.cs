using Library.Data.Entities;
using Library.Data.Persistence;
using Library.Enums;
using Microsoft.EntityFrameworkCore;

namespace Library.Data.Seeds
{
    public class LoanSeed
    {
        public static async Task Seed(LibraryDbContext context)
        {
            if (await context.Loans.AnyAsync())
                return;

            var librarian = await context.User
                .FirstAsync(u => u.Role == UserRole.librarian);

            var students = await context.User
                .Where(u => u.Role == UserRole.student)
                .ToListAsync();

            var books = await context.Books.ToListAsync();

            Random rnd = new();

            var loans = new List<Loan>
        {
            new()
            {
                Id = Guid.NewGuid(),
                LibrarianId = librarian.Id,
                UserId = students[rnd.Next(students.Count)].Id,
                BookId = books[rnd.Next(books.Count)].Id,
                DateCheckOut = DateTime.Now,
                DateReturn = DateTime.Now.AddDays(7),
                Status = LoanRole.Pending
            },
            new()
            {
                Id = Guid.NewGuid(),
                LibrarianId = librarian.Id,
                UserId = students[rnd.Next(students.Count)].Id,
                BookId = books[rnd.Next(books.Count)].Id,
                DateCheckOut = DateTime.Now,
                DateReturn = DateTime.Now.AddDays(-3),
                Status = LoanRole.Late
            }
        };

            context.Loans.AddRange(loans);
            await context.SaveChangesAsync();
        }
    }
}
