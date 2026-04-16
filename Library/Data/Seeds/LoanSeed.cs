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
                //atrasado
                Id = Guid.NewGuid(),
                LibrarianId = librarian.Id,
                UserId = students[0].Id,
                BookId = books[rnd.Next(books.Count)].Id,
                DateCheckOut = DateTime.Now.AddDays(-5),
                DueDate = DateTime.Now.AddDays(-3),
            },
            new()
            {
                //pendente
                Id = Guid.NewGuid(),
                LibrarianId = librarian.Id,
                UserId = students[1].Id,
                BookId = books[rnd.Next(books.Count)].Id,
                DateCheckOut = DateTime.Now,
                DueDate = DateTime.Now.AddDays(3),
                
            },
             new()
            {
                //retornado
                Id = Guid.NewGuid(),
                LibrarianId = librarian.Id,
                UserId = students[2].Id,
                BookId = books[rnd.Next(books.Count)].Id,
                DateCheckOut = DateTime.Now,
                DateReturn = DateTime.Now.AddDays(3),
                DueDate = DateTime.Now.AddDays(3),
            },
              new()
            {
                //retornado com atraso
                Id = Guid.NewGuid(),
                LibrarianId = librarian.Id,
                UserId = students[3].Id,
                BookId = books[rnd.Next(books.Count)].Id,
                DateCheckOut = DateTime.Now.AddDays(-3),
                DateReturn = DateTime.Now.AddDays(3),
                DueDate = DateTime.Now.AddDays(2),
            }
        };

            context.Loans.AddRange(loans);
            await context.SaveChangesAsync();
        }
    }
}
