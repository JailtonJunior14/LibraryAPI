using Library.Data.Entities;
using Library.Data.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Library.Data.Repositorys.BookRepository
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryDbContext _context;

        public BookRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAll()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<Book?> GetById(Guid id)
        {
            return await _context.Books.FindAsync(id);
        }

        public async Task<Book> Create(Book book)
        {
            await _context.AddAsync(book);
            await _context.SaveChangesAsync();

            return book;

        }

        public async Task<bool> ExistsISBN(int isbn)
        {
            bool exists = await _context.Books.AnyAsync(b => b.ISBN == isbn);
            return exists;
        }
    }
}
