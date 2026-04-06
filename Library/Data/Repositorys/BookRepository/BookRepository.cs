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
            return await _context.Books.Where(b => b.IsDeleted == false).ToListAsync();
        }

        public async Task<Book?> GetById(Guid id)
        {
            return await _context.Books.FirstOrDefaultAsync(b => b.IsDeleted == false && b.Id == id);
        }
        public async Task<IEnumerable<Book?>> GetByTitle(string title)
        {
            return await _context.Books.Where(b => b.Title == title && b.IsDeleted == false).ToListAsync();
        }
        public async Task<IEnumerable<Book?>> GetByAuthor(string author)
        {
            return await _context.Books.Where(b => b.Author == author && b.IsDeleted == false).ToListAsync();
        }

        public async Task<Book> Create(Book book)
        {
            await _context.AddAsync(book);
            await _context.SaveChangesAsync();

            return book;

        }

        public async Task<bool> ExistsISBN(long isbn)
        {
            bool exists = await _context.Books.AnyAsync(b => b.ISBN == isbn);
            return exists;
        }

        public async Task<Book> GetByISBN(long isbn)
        {
            return await _context.Books.FirstAsync(b => b.ISBN == isbn);
        }

        public async Task<Book> Update(Book book)
        {
            await _context.SaveChangesAsync();

            return book;
        }

        public async Task<bool> Delete(Guid id)
        {
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
