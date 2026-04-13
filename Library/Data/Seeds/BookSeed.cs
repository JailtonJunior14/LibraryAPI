using Library.Data.Entities;
using Library.Data.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Library.Data.Seeds
{
    public class BookSeed
    {
        public static async Task Seed(LibraryDbContext context)
        {
            if (await context.Books.AnyAsync())
                return;

            var books = new List<Book>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Author = "Bill Gates",
                Title = "Microsoft C#",
                ISBN = 123456789123,
                Publisher = "Microsoft",
                YearPublication = 2010,
                Quantity = 1
            },
            new()
            {
                Id = Guid.NewGuid(),
                Author = "João",
                Title = "Python",
                ISBN = 123456789347,
                Publisher = "Etec",
                YearPublication = 2023,
                Quantity = 5
            }
        };

            context.Books.AddRange(books);
            await context.SaveChangesAsync();
        }
    }
}
